using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class NotesUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform _transform;

    [SerializeField] float inPositionX = -136f;
    [SerializeField] float outPositionX = -79.7f;

    bool _canInteract;

    private void Awake()
    {
        Events.OnGameStart += OnGameStart;

        _transform = GetComponent<RectTransform>();

        DOMove(outPositionX, 0f);
    }

    void OnDestroy()
    {
        Events.OnGameStart -= OnGameStart;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_canInteract) return;

        DOMove(inPositionX, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_canInteract) return;

        DOMove(outPositionX, 0.5f);
    }

    void DOMove(float posX, float duration)
    {
        Vector3 pos = _transform.localPosition;
        pos.x = posX;

        _transform.DOKill();
        _transform.DOAnchorPos(pos, duration).SetEase(Ease.OutSine).Play();
    }

    void OnGameStart()
    {
        _canInteract = true;
    }
}
