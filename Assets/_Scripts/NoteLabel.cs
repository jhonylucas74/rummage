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
}
