using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] TurnsUI _turnUI;
    [SerializeField] Transform _playersContainer;

    Player _player;
    public Player Player { get => _player; }

    List<Player> _players;
    public List<Player> Players { get => _players; }

    string[] _turnOrder;
    public string[] TurnOrder { get => _turnOrder; }

    Deck _deck;
    List<Card> _gameCards;

    protected override void Awake()
    {
        base.Awake();

        _players = new List<Player>();
        _gameCards = new List<Card>();

        Events.OnPlayersUpdate += OnPlayersUpdate;
        Events.OnPrepareGame += OnPrepareGame;
        Events.OnDeckReady += OnDeckReady;
    }

    private void OnDestroy()
    {
        Events.OnPlayersUpdate -= OnPlayersUpdate;
        Events.OnPrepareGame -= OnPrepareGame;
        Events.OnDeckReady -= OnDeckReady;
    }

    void OnPrepareGame()
    {
        _deck = new Deck();
    }

    void OnDeckReady()
    {
        if (ConnectionManager.Instance.IsHost)
        {
            JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
            data.AddField("turnOrder", SetTurnOrder());
            data.AddField("cards", SetGameCards());

            int diff = 21 % Players.Count;
            int minPlayerCards = (int) Mathf.Floor(21 /  Players.Count);

            foreach (Player player in Players)
            {
                for (int i = 0; i < minPlayerCards; i ++) {
                    player.Cards.Add(_deck.GetCard().Data);
                }

                if (diff > 0) {
                    player.Cards.Add(_deck.GetCard().Data);
                    diff--;
                }
            }

            ConnectionManager.Instance.DispatchGameData(data);
            ConnectionManager.Instance.DispatchPlayers();
        }
    }

    JSONObject SetGameCards()
    {
        _gameCards = new List<Card>();
        _gameCards.Add(_deck.GetCard(CardType.Location));
        _gameCards.Add(_deck.GetCard(CardType.Weapon));
        _gameCards.Add(_deck.GetCard(CardType.Culprit));

        JSONObject obj;
        JSONObject cardsArray = new JSONObject(JSONObject.Type.ARRAY);
        for (int i = 0; i < _gameCards.Count; i++)
        {
            obj = new JSONObject(JSONObject.Type.OBJECT);
            obj.AddField("card", _gameCards[i].Data.Name);
            cardsArray.Add(obj);
        }

        return cardsArray;
    }

    JSONObject SetTurnOrder()
    {
        _players.Shuffle();
        _turnOrder = new string[_players.Count];

        JSONObject obj;
        JSONObject turnOrderArray = new JSONObject(JSONObject.Type.ARRAY);
        for (int i = 0; i < _players.Count; i++)
        {
            _turnOrder[i] = _players[i].id;
            obj = new JSONObject(JSONObject.Type.OBJECT);
            obj.AddField("playerId", _players[i].id);
            turnOrderArray.Add(obj);
        }

        return turnOrderArray;
    }

    void OnPlayersUpdate(List<Player> list)
    {
        _player = _players.Find(x => x.isMe);
    }

    public async void SetGameData(string[] turnOrder, string[] cards)
    {
        if (!ConnectionManager.Instance.IsHost)
        {
            _turnOrder = turnOrder;
            AsyncOperationHandle<Card> handler;

            foreach (string name in cards)
            {
                handler = Addressables.LoadAssetAsync<Card>(name);
                await handler.Task;
                    
                _gameCards.Add(handler.Result);
            }
        }

        await Task.Run(() => { while (_player == null) { } });
        await Task.Run(() => { while (_player.Cards.Count <= 0) { } });

        Events.OnPlayerCardsReady?.Invoke();

        AsyncOperationHandle<GameObject> playerHandler;
        AsyncOperationHandle<Sprite> spHandler;

        foreach (var player in Players)
        {
            playerHandler = Addressables.InstantiateAsync("PlayerPrefab", _playersContainer);
            spHandler = Addressables.LoadAssetAsync<Sprite>($"character{player.avatar}");
            await playerHandler.Task;
            await spHandler.Task;

            playerHandler.Result.GetComponent<PlayerScript>().Id = player.id;
            playerHandler.Result.GetComponent<SpriteRenderer>().sprite = spHandler.Result;
        }

        _turnUI.Fill(turnOrder, () => Events.OnGameStart?.Invoke());
    }
}