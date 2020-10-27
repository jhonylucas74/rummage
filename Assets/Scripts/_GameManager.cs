using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class _GameManager : MonoBehaviour {
    private SocketIOComponent socket;
    string sessionId;
    bool isHost = false;
    int playerAvatar = 0;
    string playerName = "";
    List<Player> players = new List<Player>();

    void Start () {
        socket = GetComponent<SocketIOComponent>();
        socket.On("newSession", NewSession);
        socket.On("error", OnError);
		socket.On("close", OnClose);
        // socket.On("joinupdate", Joinupdate);
        // socket.On("newGameState", NewGameState);
        Events.OnCreateSession += OnCreateSession;
        Events.OnAvatarSelect += OnAvatarSelect;
        Events.OnPlayerNameChange += OnPlayerNameChange;
        Events.OnWaitingMenu += OnWaitingMenu;
        Events.OnSessionChange += OnSessionChange;
        Events.OnJoinSession += OnJoinSession;
    }

    public void OnError(SocketIOEvent e) {
		Debug.Log(e);
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	
	public void OnClose(SocketIOEvent e) {	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}

    void OnDestroy () {
        Events.OnCreateSession -= OnCreateSession;
        Events.OnAvatarSelect -= OnAvatarSelect;
        Events.OnPlayerNameChange -= OnPlayerNameChange;
        Events.OnSessionChange -= OnSessionChange;
        Events.OnWaitingMenu -= OnWaitingMenu;
        Events.OnJoinSession -= OnJoinSession;
    }

    void OnCreateSession () {
        socket.Emit("createSession");
        socket.On("updateAvatar", OnUpdateAvatar);
        isHost = true;
    }

    void OnAvatarSelect (int avatar) {
        playerAvatar = avatar;
    }

    void OnPlayerNameChange (string name) {
        playerName = name;
    }

    void OnSessionChange (string name) {
        sessionId = name;
    }

    void OnJoinSession () {
        Debug.Log("join session: " + sessionId);
        JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
        data.AddField("sessionId", sessionId);
        socket.Emit("join", data);
    }

    void OnWaitingMenu () {
        if (isHost) {
            players[0].name = playerName;
            players[0].avatar = playerAvatar;
            Events.OnPlayersUpdate?.Invoke(players);
        } else {
            JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
            data.AddField("name", playerName);
            data.AddField("avatar", playerAvatar);

            socket.Emit("updateAvatar", data);
        }
    }

    void accessData(JSONObject obj){
        switch(obj.type){
            case JSONObject.Type.OBJECT:
                for(int i = 0; i < obj.list.Count; i++){
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    Debug.Log(key);
                    accessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach(JSONObject j in obj.list){
                    accessData(j);
                }
                break;
            case JSONObject.Type.STRING:
                Debug.Log(obj.str);
                break;
            case JSONObject.Type.NUMBER:
                Debug.Log(obj.n);
                break;
            case JSONObject.Type.BOOL:
                Debug.Log(obj.b);
                break;
            case JSONObject.Type.NULL:
                Debug.Log("NULL");
                break;
    
        }
    }

    public void NewSession (SocketIOEvent e) {
        e.data.GetField("sessionId", delegate(JSONObject data) {
            sessionId = data.str;
            players.Add(new Player("host"));
            Debug.Log(sessionId);
        }, delegate(string name) {
            Debug.LogWarning("no game sessions");
        });
    }

    // Just host receive this event
    public void OnUpdateAvatar (SocketIOEvent e) {
        string id = e.data.list[0].str;
        string name = e.data.list[1].str;
        int avatar = (int) e.data.list[2].n;

        for (int i = 0; i < players.Count; i++) {
            if (players[i].id == id) {
                players[i].name = name;
                players[i].avatar = avatar;
            }
        }

        Events.OnPlayersUpdate?.Invoke(players);
    }

    public void Joinupdate (SocketIOEvent e) {
        if (players.Count < 8) {
            e.data.GetField("userId", delegate(JSONObject data) {
                players.Add(new Player(data.str));
                Debug.Log("new player in session: " + data.str);
            }, delegate(string name) {
                Debug.LogWarning("no player");
            });
        }
    }

    public void NewGameState (SocketIOEvent e) {

    }

    void Update() {   
        if (Input.GetKeyDown("space")){
            Events.OnNextPlayerTurn?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.Q)){
            Events.OnGameStart?.Invoke(new GameState());
        }

        if (Input.GetKeyDown(KeyCode.A)){
            Events.OnCheckHand?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.S)){
            Events.OnEmptyHand?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D)){
            Events.OnFindHand?.Invoke();
        }
    }
}
