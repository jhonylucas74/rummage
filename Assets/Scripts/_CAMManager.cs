using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class _CAMManager : MonoBehaviour
{
    public CinemachineVirtualCamera general;
    public CinemachineVirtualCamera building;
    public CinemachineVirtualCamera window;
    void Start() {
        Events.OnGameStart += OnGameStart;
    }

    void OnDestroy () {
        Events.OnGameStart -= OnGameStart;
    }

    void OnGameStart () {
        general.m_Priority = 1;
        window.m_Priority = 1;
        building.m_Priority = 10;
    }
}
