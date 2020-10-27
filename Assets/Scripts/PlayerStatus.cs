using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public int position = 0;
    TMP_Text mtext;
    void Start() {
        Events.OnPlayersUpdate += OnPlayersUpdate;
        mtext = GetComponent<TMP_Text>();
    }

    void OnDestroy () {
        Events.OnPlayersUpdate -= OnPlayersUpdate;
    }

    void OnPlayersUpdate(List<Player> players) {
        if (players.Count > position) {
            mtext.text = players[position].name;
        }
    }
}
