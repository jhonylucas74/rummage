using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : Singleton<GameManager>
{
    List<Player> _players;
    public List<Player> Players { get => _players; }

    Deck _deck;
    List<Card> _gameCards;

    protected override void Awake()
    {
        base.Awake();

        _players = new List<Player>();
        _gameCards = new List<Card>();

        Events.OnDeckReady += OnDeckReady;
        Events.OnGameCardsReceived += OnGameCardsReceived;
    }

    private void Start()
    {
        _deck = new Deck();
    }

    void OnDeckReady()
    {
        if (ConnectionManager.Instance.IsHost)
        {
            _gameCards = new List<Card>();
            _gameCards.Add(_deck.GetCard(CardType.Location));
            _gameCards.Add(_deck.GetCard(CardType.Weapon));
            _gameCards.Add(_deck.GetCard(CardType.Culprit));

            foreach (Player player in Players)
            {
                player.Cards.Add(_deck.GetCard(CardType.Location).Data);
                player.Cards.Add(_deck.GetCard(CardType.Weapon).Data);
                player.Cards.Add(_deck.GetCard(CardType.Culprit).Data);
            }

            ConnectionManager.Instance.DispatchPlayers();
            ConnectionManager.Instance.DispatchGameCards(_gameCards);
        }
    }

    void OnGameCardsReceived(List<string> cards)
    {
        foreach (var c in cards)
        {
            Debug.Log(c);
        }

        if (!ConnectionManager.Instance.IsHost)
        {
            foreach (string name in cards)
            {
                Addressables.LoadAssetAsync<Card>(name).Completed += handler => _gameCards.Add(handler.Result);
            }
        }

        //TODO: Game Start
    }
}