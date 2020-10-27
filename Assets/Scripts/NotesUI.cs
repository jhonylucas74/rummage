using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NotesUI : MonoBehaviour
{
    RectTransform  uiTransform;
    Vector2 finalPosition;
    void Start() {
        uiTransform = GetComponent<RectTransform>();
        finalPosition = uiTransform.anchoredPosition;
        uiTransform.anchoredPosition = new Vector3(120f, finalPosition.y);
        Events.OnGameStart += OnGameStart;
    }

    void OnDestroy () {
        Events.OnGameStart -= OnGameStart;
    }

    void OnGameStart (GameState state) {
        DOTween.To(() => uiTransform.anchoredPosition, xy => uiTransform.anchoredPosition = xy, finalPosition, 0.5f)
        .SetEase(Ease.OutBack);
    }
}
