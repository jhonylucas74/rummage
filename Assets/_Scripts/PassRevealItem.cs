using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PassRevealItem : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        Events.OnCheckHand?.Invoke();
    }
}
