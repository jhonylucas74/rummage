using DG.Tweening;
using UnityEngine;

public class PlayerActionsUI : MonoBehaviour
{
    const float FADE_DURATION = 0.5f;

    CanvasGroup _cg;
    [SerializeField] CanvasGroup _actionsCG;
    [SerializeField] CanvasGroup _backCG;

    private void Awake()
    {
        Events.OnPlayerTurn += OnPlayerTurn;
        Events.OnMenuMove += OnMenuMove;
        Events.OnMenuPass += OnMenuPass;
        Events.OnMenuDenounce += OnMenuDenounce;
        Events.OnMenuBack += OnMenuBack;

        _cg = GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
        Events.OnPlayerTurn -= OnPlayerTurn;
        Events.OnMenuMove -= OnMenuMove;
        Events.OnMenuPass -= OnMenuPass;
        Events.OnMenuDenounce -= OnMenuDenounce;
        Events.OnMenuBack -= OnMenuBack;
    }

    void OnPlayerTurn(string pId)
    {
        if(GameManager.Instance.Player.id.Equals(pId))
        {
            _cg.interactable = true;
            _cg.blocksRaycasts = true;
            _cg.DOFade(1f, FADE_DURATION).SetEase(Ease.OutSine).Play();
        }
    }

    void OnMenuMove()
    {
        ToggleActions(false);
        ToggleBack(true);
        Events.OnPlayerMoveStart?.Invoke();
    }

    void OnMenuPass()
    {
        OnMenuMove();
    }

    void OnMenuDenounce()
    {
        OnMenuMove();
    }

    void OnMenuBack()
    {
        ToggleActions(true);
        ToggleBack(false);
    }

    void ToggleActions(bool toggle)
    {
        _actionsCG.interactable = toggle;
        _actionsCG.blocksRaycasts = toggle;
        _actionsCG.DOFade(toggle ? 1f : 0f, FADE_DURATION).SetEase(Ease.OutSine).Play();
    }

    void ToggleBack(bool toggle)
    {
        _backCG.interactable = toggle;
        _backCG.blocksRaycasts = toggle;
        _backCG.DOFade(toggle ? 1f : 0f, FADE_DURATION).SetEase(Ease.OutSine).Play();
    }
}
