using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardsManager : MonoBehaviour
{
    [SerializeField] Transform _cardsContainer;

    private void Awake()
    {
        Events.OnPlayerCardsReady += OnPlayerCardsReady;
    }

    private void OnDestroy()
    {
        Events.OnPlayerCardsReady -= OnPlayerCardsReady;
    }

    async void OnPlayerCardsReady()
    {
        Transform trans;
        Vector3 pos = Vector3.zero;
        AsyncOperationHandle<GameObject> objHandler;
        for (int i = 0; i < GameManager.Instance.Player.Cards.Count; i++)
        {
            objHandler = Addressables.InstantiateAsync(GameManager.Instance.Player.Cards[i].Name, _cardsContainer);
            await objHandler.Task;

            trans = objHandler.Result.GetComponent<Transform>();
            pos.x = -0.5f + (0.5f * i);
            pos.y = -1.25f + (0.1f * (i % 2));
            pos.z = 3.15f;

            trans.localPosition = pos;
            trans.localScale = Vector3.one;
            trans.localEulerAngles = Vector3.forward * (12.5f - 12.5f * i);
        }
    }
}