using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class DenounceCulprit : MonoBehaviour, IPointerClickHandler
{
    public int culpritIndex;
    public TMP_Text mtext;
    void Start() {
        Events.OnPlayersUpdate += OnPlayersUpdate;
    }

    void OnDestroy () {
        Events.OnPlayersUpdate -= OnPlayersUpdate;
    }

    void OnPlayersUpdate (List<Player> players) {
        for (int i = 0 ; i < players.Count; i ++) {
            if (players[i].avatar == culpritIndex) {
                mtext.text = players[i].name;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        DenounceManager.Instance.SetCulprit(culpritIndex);
    }
}
