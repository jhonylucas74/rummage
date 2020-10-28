using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Deck
{
    Queue<Card> _locationsDeck;
    Queue<Card> _weaponsDeck;
    Queue<Card> _culpritsDeck;

    public Deck()
    {
        _locationsDeck = new Queue<Card>();
        _weaponsDeck = new Queue<Card>();
        _culpritsDeck = new Queue<Card>();

        Addressables.LoadAssetAsync<DeckLibrary>("DeckLibrary").Completed += OnDeckLoadCompleted;
    }

    async void OnDeckLoadCompleted(AsyncOperationHandle<DeckLibrary> handler)
    {
        List<AssetReference> references = new List<AssetReference>();
        references.AddRange(handler.Result.Locations);
        references.AddRange(handler.Result.Weapons);
        references.AddRange(handler.Result.Culprits);
        references.Shuffle();

        AsyncOperationHandle<GameObject> cardHandler;

        for (int i = 0; i < references.Count; i++)
        {
            cardHandler = Addressables.LoadAssetAsync<GameObject>(references[i]);
            await cardHandler.Task;

            Card card = cardHandler.Result.GetComponent<Card>();

            AddCard(card.Data.Type, card);
        }

        Events.OnDeckReady?.Invoke();
    }

    void AddCard(CardType type, Card card)
    {
        switch(type)
        {
            case CardType.Location:
                _locationsDeck.Enqueue(card);
                break;
            case CardType.Weapon:
                _weaponsDeck.Enqueue(card);
                break;
            case CardType.Culprit:
                _culpritsDeck.Enqueue(card);
                break;
        }
    }

    public Card GetCard(CardType type)
    {
        Queue<Card> deck;

        switch (type)
        {
            case CardType.Location:
                deck = _locationsDeck;
                break;
            case CardType.Weapon:
                deck = _weaponsDeck;
                break;
            case CardType.Culprit:
                deck = _culpritsDeck;
                break;
            default:
                deck = null;
                break;
        }

        if (deck == null || deck.Count <= 0)
            return null;

        return deck.Dequeue();
    }
}
