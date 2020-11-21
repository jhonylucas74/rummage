using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionLabel : MonoBehaviour {
    TMP_Text mtext;

    void Start() {
        Events.OnSessionChange += OnSessionChange;
        mtext = GetComponent<TMP_Text>();
    }

    void OnDestroy () {
        Events.OnSessionChange -= OnSessionChange;
    }

    void OnSessionChange(string name) {
        Debug.Log(">>" + name);
        mtext.text = "SESSION: " + name;
    }
}
