using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class TurnsUI : MonoBehaviour
{
    public Image [] playersImages;
    RectTransform [] playersTransforms = new RectTransform[8];
    int maxPlayers = 8;
    Image activeImage;
    int turn = 0;
    int handTurn;
    public Color disabledColor = new Vector4(0.69f, 0.69f, 0.69f, 1f);
    public Color activeColor = new Vector4(1f, 1f, 1f, 1f);
    public Color noneCardColor;
    public Color checkCardColor;
    public Color sucessCardColor;

    void Start() {
        Events.OnNextPlayerTurn += OnNextPlayerTurn;
        Events.OnCheckHand += OnCheckHand;
        Events.OnEmptyHand += OnEmptyHand;
        Events.OnFindHand += OnFindHand;
        Events.OnGameStart += OnGameStart;

        hideAvatars();

        DOTween.To(() => activeImage.transform.localScale, x=> activeImage.transform.localScale = x, new Vector3(1.2f, 1.2f, 1), 0.5f)
        .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId("playerTurn").Pause();
    }

    void hideAvatars () {
        for (int i = 0; i < playersImages.Length; i++) {
            playersTransforms[i] = playersImages[i].GetComponent<RectTransform>();
            playersTransforms[i].anchoredPosition = new Vector2(playersTransforms[i].anchoredPosition.x, 20f);
        }
    }

    void OnDestroy () {
        Events.OnNextPlayerTurn -= OnNextPlayerTurn;
        Events.OnCheckHand -= OnCheckHand;
        Events.OnEmptyHand -= OnEmptyHand;
        Events.OnFindHand -= OnFindHand;
        Events.OnGameStart -= OnGameStart;
    }

    void OnGameStart (GameConfig config) {
        for (int i = 0; i < playersImages.Length; i++) {
            playAppearTween(playersTransforms[i], i * 0.05f);
        }
    }

    void playAppearTween (RectTransform playerTransform, float delay) {
        Vector2 final = new Vector2(playerTransform.anchoredPosition.x, -29f);

        DOTween.To(() => playerTransform.anchoredPosition, xy => playerTransform.anchoredPosition = xy, final, 0.5f)
            .SetEase(Ease.OutBack).SetDelay(delay);
    }

    void OnNextPlayerTurn () {
        DOTween.Play("playerTurn");
        handTurn = 0;

        if (activeImage) {
            activeImage.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        for (int i = 0; i < playersImages.Length; i++) {
            if (i != turn) {
                playersImages[i].color = disabledColor;
            } else {
                activeImage = playersImages[i];
                activeImage.color = activeColor;
            }
        }

        turn = (turn + 1) % maxPlayers;
    }

    void OnCheckHand () {
        handTurn += 1;
        playersImages[(handTurn + (turn - 1)) % maxPlayers].color = checkCardColor;
    }

    void OnEmptyHand () {
        playersImages[(handTurn + (turn - 1)) % maxPlayers].color = noneCardColor;
    }

    void OnFindHand () {
        playersImages[(handTurn + (turn - 1)) % maxPlayers].color = sucessCardColor;
    }
}
