using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ApartLabels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Events.OnPlayerMoveStart += OnPlayerMoveStart;
        Events.OnPlayerMoveSelect += OnPlayerMoveSelect;
        transform.localScale = Vector3.zero;
    }

    void OnDestroy()
    {
        Events.OnPlayerMoveStart -= OnPlayerMoveStart;
        Events.OnPlayerMoveSelect -= OnPlayerMoveSelect;
    } 

    void OnPlayerMoveSelect (int to) {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBack);
    }

    void OnPlayerMoveStart()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack);
    }
}
