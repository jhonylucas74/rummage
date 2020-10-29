using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldSelect : MonoBehaviour {
    
    [System.Serializable]
    public enum EventType {
        OnPlayerNameChange,
        OnSessionChange
    }
    public EventType inputType;

    TMP_InputField input;
    void Start() {
        input = GetComponent<TMP_InputField>();
        input.onValueChanged.AddListener(delegate { onValueChanged(); });
    }

    void onValueChanged () {
        switch (inputType) {
            case EventType.OnPlayerNameChange:
                Events.OnPlayerNameChange?.Invoke(input.text);
                break;
            case EventType.OnSessionChange:
                Events.OnSessionChange?.Invoke(input.text);
                break;
        }
    }
}
