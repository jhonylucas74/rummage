using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DenounceWeapon : MonoBehaviour, IPointerClickHandler
{
    public int weaponIndex;

    public void OnPointerClick(PointerEventData eventData) {
        DenounceManager.Instance.SetWeapon(weaponIndex);
    }
}
