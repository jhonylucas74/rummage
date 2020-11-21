using DG.Tweening;
using UnityEngine;

public class PlayerActionsUI : MonoBehaviour
{
    const float FADE_DURATION = 0.5f;

    CanvasGroup _cg;
    [SerializeField] CanvasGroup _actionsCG;
    [SerializeField] CanvasGroup _backCG;
    [SerializeField] GameObject move;
    [SerializeField] GameObject denounce;

    private void Awake()
    {
        Events.OnPlayerTurn += OnPlayerTurn;
        Events.OnMenuMove += OnMenuMove;
        Events.OnMenuPass += OnMenuPass;
        Events.OnMenuDenounce += OnMenuDenounce;
        Events.OnMenuBack += OnMenuBack;
        Events.OnPlayerMoveEnd += OnPlayerMoveEnd;

        _cg = GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
        Events.OnPlayerTurn -= OnPlayerTurn;
        Events.OnMenuMove -= OnMenuMove;
        Events.OnMenuPass -= OnMenuPass;
        Events.OnMenuDenounce -= OnMenuDenounce;
        Events.OnMenuBack -= OnMenuBack;
        Events.OnPlayerMoveEnd -= OnPlayerMoveEnd;
    }

    void OnPlayerTurn(string pId)
    {
        move.SetActive(true);
        denounce.SetActive(false);

        if(GameManager.Instance.Player.id.Equals(pId))
        {
            _cg.interactable = true;
            _cg.blocksRaycasts = true;
            _cg.DOFade(1f, FADE_DURATION).SetEase(Ease.OutSine).Play();
        } else {
            _cg.interactable = false;
            _cg.blocksRaycasts = false;
            _cg.DOFade(0f, FADE_DURATION).SetEase(Ease.OutSine).Play();
        }
    }

    void OnMenuMove()
    {
        move.SetActive(false);
        Events.OnPlayerMoveStart?.Invoke();
    }

    void OnPlayerMoveEnd (bool valid) {
        denounce.SetActive(valid);
    }

    void OnMenuPass()
    {
        Events.OnNextPlayerTurn?.Invoke();
    }

    void OnMenuDenounce()
    {
        DenounceManager.Instance.StartDenounce();
        _cg.interactable = false;
        _cg.blocksRaycasts = false;
        _cg.DOFade(0f, FADE_DURATION).SetEase(Ease.OutSine).Play();
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
