using System.Collections.Generic;
using UnityEngine;

public static class Events {
    public static GameStateEvent OnGameStart;
    public static SimpleEvent OnNextPlayerTurn;
    public static SimpleEvent OnCheckHand;
    public static SimpleEvent OnEmptyHand;
    public static SimpleEvent OnFindHand;
    public static SimpleEvent OnInitialMenu;
    public static SimpleEvent OnCreateSession;
    public static SimpleEvent OnJoinSession;
    public static SimpleEvent OnAvatarsMenu;
    public static SimpleEvent OnWaitingMenu;
    public static SimpleEvent OnJoinMenu;
    public static IntEvent OnAvatarSelect;
    public static StringEvent OnPlayerNameChange;
    public static StringEvent OnSessionChange;
    public static PlayersEvent OnPlayersUpdate;

    public static SimpleEvent OnDeckReady;
}

public delegate void PlayersEvent(List<Player> players);
public delegate void GameStateEvent(GameState state);
public delegate void SimpleEvent();
public delegate void IntEvent(int i);
public delegate void FloatEvent(float f);
public delegate void BoolEvent(bool b);
public delegate void StringEvent(string s);
public delegate void Vector3Event(Vector3 v3);
