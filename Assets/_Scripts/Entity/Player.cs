using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public string name;
    public string id;
    public int avatar;
    public bool isMe = false;

    List<CardData> _cards;
    public List<CardData> Cards { get => _cards; }

    public Player(string pid) {
        id = pid;
        _cards = new List<CardData>();
    }

    public Player(JSONObject obj) {
        id = obj.list[0].str;
        name = obj.list[1].str;
        avatar = (int) obj.list[2].n;
        _cards = new List<CardData>();

        if (obj.list.Count == 4) {
            for (int i = 0; i < obj.list[3].Count; i++) {
                CardType ctype = (CardType) obj.list[3][i][0].n;
                string cName = obj.list[3][i][1].str;
                _cards.Add(new CardData(ctype, cName));
            }
        }
    }

    public JSONObject getJSON () {
        JSONObject player = new JSONObject(JSONObject.Type.ARRAY);
        player.Add(id);
        player.Add(name);
        player.Add(avatar);

        JSONObject cards = new JSONObject(JSONObject.Type.ARRAY);

        for (int i = 0; i < _cards.Count; i++) {
            JSONObject card = new JSONObject(JSONObject.Type.ARRAY);
            card.Add((int) _cards[i].Type);
            card.Add(_cards[i].Name);
            cards.Add(card);
        }

        player.Add(cards);
        return player;
    }
}
