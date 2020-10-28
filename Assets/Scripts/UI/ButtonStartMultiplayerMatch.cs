using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonStartMultiplayerMatch : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.StartMatch();
    }
}