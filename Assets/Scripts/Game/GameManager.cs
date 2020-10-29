using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] TurnsUI _turnUI;
    [SerializeField] Transform _cardsContainer;

    Player _player;
    List<Player> _players;
    public List<Player> Players { get => _players; }

    string[] _turnOrder;

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

    void OnPrepareGame()
    {
        _deck = new Deck();
    }

    void OnDeckReady()
    {
        if (ConnectionManager.Instance.IsHost)
        {
            foreach (Player player in Players)
            {
                player.Cards.Add(_deck.GetCard(CardType.Location).Data);
                player.Cards.Add(_deck.GetCard(CardType.Weapon).Data);
                player.Cards.Add(_deck.GetCard(CardType.Culprit).Data);
            }

            JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
            data.AddField("turnOrder", SetTurnOrder());
            data.AddField("cards", SetGameCards());

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

        Transform trans;
        Vector3 pos = Vector3.zero;
        AsyncOperationHandle<GameObject> objHandler;
        for (int i = 0; i < _player.Cards.Count; i++)
        {
            objHandler = Addressables.InstantiateAsync(_player.Cards[i].Name, _cardsContainer);
            await objHandler.Task;

            trans = objHandler.Result.GetComponent<Transform>();
            pos.x = -0.5f + (0.5f * i);
            pos.y = -1.25f + (0.1f * (i % 2));
            pos.z = 3.15f;

            trans.localPosition = pos;
            trans.localScale = Vector3.one;
            trans.localEulerAngles = Vector3.forward * (12.5f - 12.5f * i);
        }

        _turnUI.Fill(turnOrder, () => Events.OnGameStart?.Invoke());
    }
}