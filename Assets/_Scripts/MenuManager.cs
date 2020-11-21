using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ModeOptions;
    public GameObject AvatarSelect;
    public GameObject WaitingRoom;
    public GameObject JoinMenu;
    public GameObject StartBtn;
    AudioSource _clickSound; 

    void Start() {
        AvatarSelect.SetActive(false);
        WaitingRoom.SetActive(false);
        JoinMenu.SetActive(false);

        _clickSound = GetComponent<AudioSource>();

        Events.OnInitialMenu += OnInitialMenu;
        Events.OnAvatarsMenu += OnAvatarsMenu;
        Events.OnGameStart += OnGameStart;
        Events.OnWaitingMenu += OnWaitingMenu;
        Events.OnJoinMenu += OnJoinMenu;
        Events.OnPrepareGame += OnPrepareGame;
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
        _clickSound.Play();
    }

    void OnGameStart () {
        gameObject.SetActive(false);
        _clickSound.Play();
    }

    void OnAvatarsMenu () {
        ModeOptions.SetActive(false);
        AvatarSelect.SetActive(true);
        WaitingRoom.SetActive(false);
        JoinMenu.SetActive(false);
        _clickSound.Play();
    }

    void OnWaitingMenu () {
        ModeOptions.SetActive(false);
        AvatarSelect.SetActive(false);
        WaitingRoom.SetActive(true);
        JoinMenu.SetActive(false);
        _clickSound.Play();
        StartBtn.SetActive(ConnectionManager.Instance.IsHost);
    }

    void OnJoinMenu () {
        ModeOptions.SetActive(false);
        AvatarSelect.SetActive(false);
        WaitingRoom.SetActive(false);
        JoinMenu.SetActive(true);
        _clickSound.Play();
    }

    void OnPrepareGame () {
        ModeOptions.SetActive(false);
        AvatarSelect.SetActive(false);
        WaitingRoom.SetActive(false);
        JoinMenu.SetActive(false);
    }
}
