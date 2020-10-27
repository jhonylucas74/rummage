using System.Collections.Generic;
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

        AsyncOperationHandle<Card> cardHandler;

        for (int i = 0; i < references.Count; i++)
        {
            cardHandler = Addressables.LoadAssetAsync<Card>(references[i]);
            await cardHandler.Task;

            AddCard(cardHandler.Result.Data.Type, cardHandler.Result);
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
        switch (type)
        {
            case CardType.Location:
                return _locationsDeck.Dequeue();
            case CardType.Weapon:
                return _weaponsDeck.Dequeue();
            case CardType.Culprit:
                return _culpritsDeck.Dequeue();
            default:
                return null;
        }
    }
}
