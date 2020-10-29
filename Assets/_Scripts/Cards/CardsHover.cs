using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardsHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform _transform;
    Vector3[] _originalPositions;
    Vector3[] _originalRotations;

    bool _firstRun = true;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_firstRun)
        {
            _originalPositions = new Vector3[_transform.childCount];
            _originalRotations = new Vector3[_transform.childCount];
        }

        Vector3 position;
        Transform cTrans;
        for(int i = 0; i < _transform.childCount; i++)
        {
            cTrans = _transform.GetChild(i);
            cTrans.DOKill();

            if (_firstRun)
            {
                _originalPositions[i] = cTrans.localPosition;
                _originalRotations[i] = cTrans.localEulerAngles;
            }

            position.x = -0.6f + (0.6f * i);
            position.y = -0.3f;
            position.z = cTrans.localPosition.z;

            cTrans.DOLocalMove(position, 1f).SetEase(Ease.OutSine).Play();
            cTrans.DOLocalRotate(Vector3.zero, 1f).SetEase(Ease.OutSine).Play();
        }

        _firstRun = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Transform cTrans;
        for (int i = 0; i < _transform.childCount; i++)
        {
            cTrans = _transform.GetChild(i);
            cTrans.DOKill();

            cTrans.DOLocalMove(_originalPositions[i], 1f).SetEase(Ease.OutSine).Play();
            cTrans.DOLocalRotate(_originalRotations[i], 1f).SetEase(Ease.OutSine).Play();
        }
    }
}