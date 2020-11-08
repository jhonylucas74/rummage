using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectApartMove : MonoBehaviour, IPointerClickHandler
{
    public int apart = 0;

    public void OnPointerClick(PointerEventData eventData) {
        //Events.OnPlayerMoveSelect?.Invoke(apart);
        ConnectionManager.Instance.SendPlayerMovement(GameManager.Instance.Player.id, apart);
    }
}
