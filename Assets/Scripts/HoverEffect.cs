using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [System.Serializable]
    public enum ClickEventType {
        OnInitialMenu,
        OnAvatarsMenu
    }

    public ClickEventType clickEvent;

    RectTransform textTransform;
    float originalX;

    void Start () {
        textTransform = GetComponent<RectTransform>();
        originalX = textTransform.anchoredPosition.x;
    }

    public void OnPointerClick(PointerEventData eventData) {
        switch (clickEvent) {
            case ClickEventType.OnInitialMenu:
                Events.OnInitialMenu?.Invoke();
                break;
            case ClickEventType.OnAvatarsMenu:
                Events.OnAvatarsMenu?.Invoke();
                break;
        }
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
