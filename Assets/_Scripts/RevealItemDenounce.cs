using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class RevealItemDenounce : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (_text.text != "") {
            ConnectionManager.Instance.SendStopDenounce(_text.text);
        }
    }
}
