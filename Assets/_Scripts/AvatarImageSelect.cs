using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class AvatarImageSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public int avatar;
    Image avatarImage;
    public Color disabledColor = new Vector4(0.69f, 0.69f, 0.69f, 1f);
    public Color activeColor = new Vector4(1f, 1f, 1f, 1f);

    void Start() {
        Events.OnAvatarSelect += OnAvatarSelect;
        avatarImage = GetComponent<Image>();

        if (avatar == 0) {
            avatarImage.color = activeColor;
        } else {
            avatarImage.color = disabledColor;
        }
    }

    void OnDestroy () {
        Events.OnAvatarSelect -= OnAvatarSelect;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        DOTween.To(() => avatarImage.transform.localScale, x=> avatarImage.transform.localScale = x, new Vector3(1.1f, 1.1f, 1), 0.5f)
        .SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData) {
        DOTween.To(() => avatarImage.transform.localScale, x=> avatarImage.transform.localScale = x, new Vector3(1f, 1f, 1f), 0.5f)
        .SetEase(Ease.OutBack);
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        Events.OnAvatarSelect?.Invoke(avatar);
    }

    void OnAvatarSelect (int newAvatar) {
        if (newAvatar == avatar) {
            avatarImage.color = activeColor;
        } else {
            avatarImage.color = disabledColor;
        }
     }
}
