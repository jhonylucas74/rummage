using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ModeOptions;
    public GameObject AvatarSelect;
    void Start() {
        AvatarSelect.SetActive(false);
        Events.OnInitialMenu += OnInitialMenu;
        Events.OnAvatarsMenu += OnAvatarsMenu;
    }

    void OnDestroy () {
        Events.OnInitialMenu -= OnInitialMenu;
        Events.OnAvatarsMenu -= OnAvatarsMenu;
    }

    void OnInitialMenu () {
        ModeOptions.SetActive(true);
        AvatarSelect.SetActive(false);
    }

    void OnAvatarsMenu () {
        ModeOptions.SetActive(false);
        AvatarSelect.SetActive(true);
    }

}
