using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class TurnsUI : MonoBehaviour
{
    public Image [] playersImages;
    Image activeImage;
    int turn;
    int handTurn;
    public Color disabledColor = new Vector4(0.69f, 0.69f, 0.69f, 1f);
    public Color activeColor = new Vector4(1f, 1f, 1f, 1f);
    public Color noneCardColor;
    public Color checkCardColor;
    public Color sucessCardColor;
    void Start() {
        Events.OnSetPlayerTurn += OnSetPlayerTurn;
        Events.OnCheckHand += OnCheckHand;
        Events.OnEmptyHand += OnEmptyHand;
        Events.OnFindHand += OnFindHand;

        DOTween.To(() => activeImage.transform.localScale, x=> activeImage.transform.localScale = x, new Vector3(1.2f, 1.2f, 1), 0.5f)
        .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId("playerTurn").Pause();
    }

    void OnDestroy () {
        Events.OnSetPlayerTurn -= OnSetPlayerTurn;
    }

    void OnSetPlayerTurn (int player) {
        DOTween.Play("playerTurn");
        turn = player;
        handTurn = 0;

        if (activeImage) {
            activeImage.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        for (int i = 0; i < playersImages.Length; i++) {
            if (i != player) {
                playersImages[i].color = disabledColor;
            } else {
                activeImage = playersImages[i];
                activeImage.color = activeColor;
            }
        }
    }

    void OnCheckHand () {
        handTurn += 1;
        playersImages[(handTurn + turn) % 8].color = checkCardColor;
    }

    void OnEmptyHand () {
        playersImages[(handTurn + turn) % 8].color = noneCardColor;
    }

    void OnFindHand () {
        playersImages[(handTurn + turn) % 8].color = sucessCardColor;
    }

    void Update()
    {
        if (Input.GetKeyDown("space")){
            Events.OnSetPlayerTurn?.Invoke((turn + 1) % 8);
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
