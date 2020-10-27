using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public string name;
    public string id;
    public int avatar;

    List<Card> _cards;
    public List<Card> Cards { get => _cards; }

    public Player(string pid) {
        id = pid;

        _cards = new List<Card>();
    }

    public Player(JSONObject obj) {
        id = obj.list[0].str;
        name = obj.list[1].str;
        avatar = (int) obj.list[2].n;
    }
}
