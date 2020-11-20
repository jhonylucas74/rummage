using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class NoteLabel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [System.Serializable]
    public enum LabelType { Location, Weapon, Culprit }
    public LabelType type;
    public int playerIndex;

    RectTransform textTransform;
    TMP_Text mtext;
    float originalX;
    bool isChecked = false;

    void Start() {
        textTransform = GetComponent<RectTransform>();
        originalX = textTransform.anchoredPosition.x;
        mtext = GetComponent<TMP_Text>();

        Events.OnPlayersUpdate += OnPlayersUpdate;
        Events.OnPlayerCardsReady += OnPlayerCardsReady;
    }

    void OnDestroy () {
        Events.OnPlayersUpdate -= OnPlayersUpdate;
        Events.OnPlayerCardsReady -= OnPlayerCardsReady;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (isChecked) {
            mtext.fontStyle = FontStyles.Bold;
        } else {
            mtext.fontStyle = FontStyles.Strikethrough;
        }

        isChecked = !isChecked;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Vector2 final = new Vector2(originalX + 10f, textTransform.anchoredPosition.y);
        DOTween.To(() => textTransform.anchoredPosition, xy => textTransform.anchoredPosition = xy, final, 0.5f)
            .SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Vector2 final = new Vector2(originalX, textTransform.anchoredPosition.y);
        DOTween.To(() => textTransform.anchoredPosition, xy => textTransform.anchoredPosition = xy, final, 0.5f)
            .SetEase(Ease.OutBack);
    }

    void OnPlayersUpdate (List<Player> players) {
        if (type == LabelType.Culprit) {
            for (int i = 0 ; i < players.Count; i ++) {
                if (players[i].avatar == playerIndex) {
                    mtext.text = players[i].name;
                }
            }
        }
    }

    void OnPlayerCardsReady() {
        for (int i = 0; i < GameManager.Instance.Player.Cards.Count; i++) {
            if (mtext.text == GameManager.Instance.Player.Cards[i].Name) {
                mtext.fontStyle = FontStyles.Strikethrough;
            }
        }
    }
}
