using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    List<Player> _players;
    public List<Player> Players { get => _players; }

    Deck _deck;
    List<Card> _gameCards;

    protected override void Awake()
    {
        base.Awake();

        Events.OnDeckReady += OnDeckReady;
    }

    private void Start()
    {
        _deck = new Deck();
    }

    void OnDeckReady()
    {
        _gameCards = new List<Card>();
        _gameCards.Add(_deck.GetCard(CardType.Location));
        _gameCards.Add(_deck.GetCard(CardType.Weapon));
        _gameCards.Add(_deck.GetCard(CardType.Culprit));

        if (ConnectionManager.Instance.isHost)
        {
            foreach (Player player in Players)
            {
                player.Cards.Add(_deck.GetCard(CardType.Location).Data);
                player.Cards.Add(_deck.GetCard(CardType.Weapon).Data);
                player.Cards.Add(_deck.GetCard(CardType.Culprit).Data);
            }
        }
    }
}