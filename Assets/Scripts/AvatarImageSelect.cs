using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvatarImageSelect : MonoBehaviour, IPointerClickHandler {
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
