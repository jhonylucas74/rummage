using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;


public class CardsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject labelPrefab;
    public GameObject container;
    public Canvas CanvasObject;

    RectTransform _transform;
    float inPositionX = 160f;
    float outPositionX = -50f;

    private void Awake()
    {
        Events.OnPlayerCardsReady += OnPlayerCardsReady;
        CanvasObject.enabled = false;

        _transform = GetComponent<RectTransform>();
        DOMove(outPositionX, 0f);
    }

    private void OnDestroy()
    {
        Events.OnPlayerCardsReady -= OnPlayerCardsReady;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DOMove(inPositionX, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOMove(outPositionX, 0.5f);
    }

    void DOMove(float posX, float duration)
    {
        _transform.DOKill();
        _transform.DOAnchorPosX(posX, duration).SetEase(Ease.OutSine).Play();
    }

    int getIndexByCulpritName (string name) {
        switch (name) {
            case "Comunist":
                return 0;
            case "E-girl":
                return 1;
            case "Raged":
                return 2;
            case "Pet Dad":
                return 3;
            case "Homeless":
                return 4;
            case "Emo":
                return 5;
            case "Bearded":
                return 6;
            case "Competitive":
            default:
                return 7;
        }
    }

    void OnPlayerCardsReady()
    {
        for (int i = 0; i < GameManager.Instance.Player.Cards.Count; i++)
        {
            TextMeshProUGUI textMesh = Instantiate(labelPrefab, container.transform).GetComponent<TextMeshProUGUI>();
            textMesh.text = GameManager.Instance.Player.Cards[i].Name;

            if (GameManager.Instance.Player.Cards[i].Type == CardType.Culprit) {
                for (int j = 0; j < GameManager.Instance.Players.Count; j++) {
                    if (GameManager.Instance.Players[j].avatar == getIndexByCulpritName(textMesh.text)) {
                        textMesh.text = GameManager.Instance.Players[j].name;
                    }
                }
            }
        }

        RectTransform rect = container.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(216.6417f, GameManager.Instance.Player.Cards.Count * 21f);
        CanvasObject.enabled = true;
    }
}