using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ModeOptions;
    public GameObject AvatarSelect;
    public GameObject WaitingRoom;
    public GameObject JoinMenu;

    void Start() {
        AvatarSelect.SetActive(false);
        WaitingRoom.SetActive(false);
        JoinMenu.SetActive(false);

        Events.OnInitialMenu += OnInitialMenu;
        Events.OnAvatarsMenu += OnAvatarsMenu;
        Events.OnGameStart += OnGameStart;
        Events.OnWaitingMenu += OnWaitingMenu;
        Events.OnJoinMenu += OnJoinMenu;
    }

    void OnDestroy () {
        Events.OnInitialMenu -= OnInitialMenu;
        Events.OnAvatarsMenu -= OnAvatarsMenu;
        Events.OnGameStart -= OnGameStart;
        Events.OnWaitingMenu -= OnWaitingMenu;
        Events.OnJoinMenu -= OnJoinMenu;
    }

    void OnInitialMenu () {
        ModeOptions.SetActive(true);
        AvatarSelect.SetActive(false);
        WaitingRoom.SetActive(false);
        JoinMenu.SetActive(false);
    }

    void OnGameStart (GameState state) {
        gameObject.SetActive(false);
    }

    void OnAvatarsMenu () {
        ModeOptions.SetActive(false);
        AvatarSelect.SetActive(true);
        WaitingRoom.SetActive(false);
        JoinMenu.SetActive(false);
    }

    void OnWaitingMenu () {
        ModeOptions.SetActive(false);
        AvatarSelect.SetActive(false);
        WaitingRoom.SetActive(true);
        JoinMenu.SetActive(false);
    }

    void OnJoinMenu () {
        ModeOptions.SetActive(false);
        AvatarSelect.SetActive(false);
        WaitingRoom.SetActive(false);
        JoinMenu.SetActive(true);
    }
}
