using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {   
        if (Input.GetKeyDown("space")){
            Events.OnNextPlayerTurn?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.Q)){
            Events.OnGameStart?.Invoke(new GameConfig());
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
