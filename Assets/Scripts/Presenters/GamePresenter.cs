using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resource1QuantityText;
    [SerializeField] TextMeshProUGUI resource2QuantityText;
    [SerializeField] TextMeshProUGUI resource3QuantityText;
    [SerializeField] TextMeshProUGUI cardsInDeckText;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] List<Image> modSlots;

    [SerializeField] ResourceData resource1Data;
    [SerializeField] ResourceData resource2Data;
    [SerializeField] ResourceData resource3Data;
    [SerializeField] GameData gameData;

    private void Awake()
    {
        EventBus.onResourceModifiedEvent += UpdateResources;
        EventBus.onTurnEndedEvent += UpdateGameUI;
        EventBus.onTurnInitializedEvent += UpdateGameUI;
        EventBus.onGameInitializedEvent += UpdateGameUI;
    }
    private void OnDestroy()
    {
        EventBus.onResourceModifiedEvent -= UpdateResources;
        EventBus.onTurnEndedEvent -= UpdateGameUI;
        EventBus.onTurnInitializedEvent -= UpdateGameUI;
    }

    public void UpdateGameUI() 
    {
        resource1QuantityText.text = resource1Data.ResourceQuantity.ToString();
        resource2QuantityText.text = resource2Data.ResourceQuantity.ToString();
        resource3QuantityText.text = resource3Data.ResourceQuantity.ToString();

        for (int i = 0; i < gameData.currentModifiers.Count; i++)
        {
            Image icon = modSlots[i].GetComponentsInChildren<Image>()[1];

            icon.sprite = gameData.currentModifiers[i].icon;
            icon.enabled = true;
        }

        scoreLabel.text = gameData.TotalScore.ToString();
        cardsInDeckText.text = gameData.gameCardPile.Count.ToString();
    }

    private void UpdateResources(ResourceData data) 
    {
        UpdateGameUI();
    }
}
