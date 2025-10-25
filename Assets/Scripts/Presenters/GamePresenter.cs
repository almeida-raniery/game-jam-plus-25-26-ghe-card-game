using TMPro;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resource1QuantityText;
    [SerializeField] TextMeshProUGUI resource2QuantityText;
    [SerializeField] TextMeshProUGUI resource3QuantityText;
    [SerializeField] TextMeshProUGUI cardsInDeckText;

    [SerializeField] ResourceData resource1Data;
    [SerializeField] ResourceData resource2Data;
    [SerializeField] ResourceData resource3Data;
    [SerializeField] GameData gameData;

    private void Awake()
    {
        EventBus.onResourceModifiedEvent += UpdateResources;
        EventBus.onTurnEndedEvent += UpdateGameUI;
    }

    private void OnDestroy()
    {
        EventBus.onResourceModifiedEvent -= UpdateResources;
        EventBus.onTurnEndedEvent -= UpdateGameUI;
    }

    public void UpdateGameUI() 
    {
        resource1QuantityText.text = resource1Data.ResourceQuantity.ToString();
        resource2QuantityText.text = resource2Data.ResourceQuantity.ToString();
        resource3QuantityText.text = resource3Data.ResourceQuantity.ToString();

        cardsInDeckText.text = gameData.gameCardPile.Count.ToString();
    }

    private void UpdateResources(ResourceData data) 
    {
        UpdateGameUI();
    }
}
