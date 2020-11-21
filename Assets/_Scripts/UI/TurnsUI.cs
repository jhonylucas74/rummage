using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TurnsUI : MonoBehaviour
{
    RectTransform _transform;

    List<Image> playersImages;
    RectTransform [] playersTransforms = new RectTransform[8];
    int maxPlayers = 8;
    Image activeImage;
    
    int turn = 0;
    int handTurn = 0;

    public Color disabledColor = new Vector4(0.69f, 0.69f, 0.69f, 1f);
    public Color activeColor = new Vector4(1f, 1f, 1f, 1f);
    public Color noneCardColor;
    public Color checkCardColor;
    public Color sucessCardColor;

    void Awake() {
        Events.OnNextPlayerTurn += OnNextPlayerTurn;
        Events.OnCheckHand += OnCheckHand;
        Events.OnEmptyHand += OnEmptyHand;
        Events.OnFindHand += OnFindHand;
        Events.OnGameStart += OnGameStart;
        Events.OnPlayerTurn += OnPlayerTurn;
        Events.OnReceiveDenounce += OnReceiveDenounce;
        Events.OnStopDenounce += OnStopDenounce;

        _transform = GetComponent<RectTransform>();
        playersImages = new List<Image>();
    }

    void hideAvatars () {
        for (int i = 0; i < playersImages.Count; i++) {
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
        Events.OnPlayerTurn -= OnPlayerTurn;
        Events.OnReceiveDenounce -= OnReceiveDenounce;
        Events.OnStopDenounce -= OnStopDenounce;
    }

    void OnGameStart () {
        for (int i = 0; i < playersImages.Count; i++) {
            playAppearTween(playersTransforms[i], i * 0.05f);
        }

        OnNextPlayerTurn();
    }

    void playAppearTween (RectTransform playerTransform, float delay) {
        Vector2 final = new Vector2(playerTransform.anchoredPosition.x, -29f);

        DOTween.To(() => playerTransform.anchoredPosition, xy => playerTransform.anchoredPosition = xy, final, 0.5f)
            .SetEase(Ease.OutBack).SetDelay(delay);
    }

    void OnNextPlayerTurn () {
        ConnectionManager.Instance.SendCurrentPlayerTurn(GameManager.Instance.TurnOrder[(turn + 1) % maxPlayers]);
    }

    void OnPlayerTurn(string pId) {
        turn = System.Array.IndexOf(GameManager.Instance.TurnOrder, pId);
        handTurn = turn;
        DOTween.Play("playerTurn");

        if (activeImage) {
            activeImage.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        for (int i = 0; i < playersImages.Count; i++) {
            if (i != turn) {
                playersImages[i].color = disabledColor;
            } else {
                activeImage = playersImages[i];
                activeImage.color = activeColor;
            }
        }
    }

    void OnReceiveDenounce(string pId) {
        handTurn = System.Array.IndexOf(GameManager.Instance.TurnOrder, pId);

        for (int i = 0; i < playersImages.Count; i++) {
            if (i == handTurn) {
                playersImages[i].color = checkCardColor;
            }
        }

        if (ConnectionManager.Instance.IsLocalUser(pId)) {
            if (handTurn == turn) {
                Events.OnNextPlayerTurn?.Invoke();
            }
        }
    }

    void OnStopDenounce (string name) {
        if (turn == System.Array.IndexOf(GameManager.Instance.TurnOrder, GameManager.Instance.Player.id)) {
            DenounceManager.Instance.ShowReveledCard("You received: " + name);
        } else {
            string id = GameManager.Instance.TurnOrder[handTurn];

            for (int i = 0; i < GameManager.Instance.Players.Count; i++) {
                if (GameManager.Instance.Players[i].id == id) {
                    DenounceManager.Instance.ShowReveledCard("Player " + GameManager.Instance.Players[i].name + " revealed one item.");
                }
            }
        }
    }

    void OnCheckHand () {
        ConnectionManager.Instance.SendReceiveDenounce(GameManager.Instance.TurnOrder[(handTurn + 1) % maxPlayers]);
    }

    void OnEmptyHand () {
        playersImages[(handTurn + turn) % maxPlayers].color = noneCardColor;
    }

    void OnFindHand () {
        playersImages[(handTurn + turn) % maxPlayers].color = sucessCardColor;
    }

    public async void Fill(string[] turnOrder, SimpleEvent callback = null)
    {
        Image image;
        AsyncOperationHandle<GameObject> goHandler;
        AsyncOperationHandle<Sprite> spriteHandler;
        int portraitIndex;
        maxPlayers = turnOrder.Length;

        for (int i = 0; i < turnOrder.Length; i++)
        {
            portraitIndex = GameManager.Instance.Players.Find(x => x.id == turnOrder[i]).avatar;
            spriteHandler = Addressables.LoadAssetAsync<Sprite>($"portrait{portraitIndex}");
            goHandler = Addressables.InstantiateAsync("playerPortrait", _transform);

            Task[] tasks = new Task[2] { goHandler.Task, spriteHandler.Task };
            await Task.WhenAll(tasks);

            image = goHandler.Result.GetComponent<Image>();
            image.sprite = spriteHandler.Result;
            playersImages.Add(image);
        }

        hideAvatars();

        DOTween.To(() => activeImage.transform.localScale, x => activeImage.transform.localScale = x, new Vector3(1.2f, 1.2f, 1), 0.5f)
        .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId("playerTurn").Pause();

        callback?.Invoke();
    }
}
