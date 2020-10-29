using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotesHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform _transform;

    [SerializeField] float inPositionX = -136f;
    [SerializeField] float outPositionX = 104f;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();

        OnPointerExit(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 pos = _transform.localPosition;
        pos.x = inPositionX;

        _transform.DOKill();
        _transform.DOAnchorPos(pos, 1f).SetEase(Ease.OutSine).Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 pos = _transform.localPosition;
        pos.x = outPositionX;

        _transform.DOKill();
        _transform.DOAnchorPos(pos, 1f).SetEase(Ease.OutSine).Play();
    }
}