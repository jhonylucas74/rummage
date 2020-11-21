using UnityEngine;

public class Card : MonoBehaviour
{
    public CardData Data;

    public Card(CardData data) {
        Data = data;
    }
}

[System.Serializable]
public class CardData
{
    public CardType Type;
    public string Name;

    public CardData(CardType t, string n) {
        Type = t;
        Name = n;
    }
}