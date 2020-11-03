using System.Collections.Generic;
using UnityEngine;

public static class Events {
    public static SimpleEvent OnNextPlayerTurn;
    public static SimpleEvent OnCheckHand;
    public static SimpleEvent OnEmptyHand;
    public static SimpleEvent OnFindHand;
    
    public static IntEvent OnAvatarSelect;
    public static StringEvent OnPlayerNameChange;
    public static StringEvent OnSessionChange;
    public static PlayersEvent OnPlayersUpdate;

    #region Menu
    public static SimpleEvent OnInitialMenu;
    public static SimpleEvent OnCreateSession;
    public static SimpleEvent OnJoinSession;
    public static SimpleEvent OnAvatarsMenu;
    public static SimpleEvent OnWaitingMenu;
    public static SimpleEvent OnJoinMenu;
    public static SimpleEvent OnPrepareGame;
    #endregion
    #region Game
    public static SimpleEvent OnGameStart;
    public static SimpleEvent OnGameEnd;
    public static SimpleEvent OnDeckReady;
    public static ListStringEvent OnGameCardsReceived;
    public static SimpleEvent OnPlayerCardsReady;

    public static StringEvent OnPlayerTurn;

    public static SimpleEvent OnMenuMove;
    public static SimpleEvent OnMenuDenounce;
    public static SimpleEvent OnMenuPass;
    public static SimpleEvent OnMenuBack;
    #endregion

    public static SimpleEvent OnPlayerMoveStart;
    public static IntEvent OnPlayerMoveSelect;
}


public delegate void GameStateEvent(GameState state);
public delegate void SimpleEvent();
public delegate void IntEvent(int i);
public delegate void FloatEvent(float f);
public delegate void BoolEvent(bool b);
public delegate void StringEvent(string s);
public delegate void Vector3Event(Vector3 v3);

public delegate void ListStringEvent(List<string> list);
public delegate void ListCardEvent(List<Card> list);
public delegate void PlayersEvent(List<Player> players);