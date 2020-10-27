using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    List<Player> _players;
    public List<Player> Players { get => _players; }

    Deck _deck;
    List<Card> _gameCards;

    private void Awake()
    {
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

        foreach (Player player in Players)
        {
            player.Cards.Add(_deck.GetCard(CardType.Location));
            player.Cards.Add(_deck.GetCard(CardType.Weapon));
            player.Cards.Add(_deck.GetCard(CardType.Culprit));
        }
    }
}