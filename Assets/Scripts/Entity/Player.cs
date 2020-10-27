using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public string name;
    public string id;
    public int avatar;

    public Player(string pid) {
        id = pid;
    }

    public Player(JSONObject obj) {
        id = obj.list[0].str;
        name = obj.list[1].str;
        avatar = (int) obj.list[2].n;
    }
}
