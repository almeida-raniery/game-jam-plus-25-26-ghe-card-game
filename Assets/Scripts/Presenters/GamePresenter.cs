using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePresenter : MonoBehaviour
{
    [Header("Game - setup")]
    [SerializeField] TextMeshProUGUI resource1QuantityText;
    [SerializeField] TextMeshProUGUI resource2QuantityText;
    [SerializeField] TextMeshProUGUI resource3QuantityText;
    [SerializeField] TextMeshProUGUI cardsInDeckText;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] List<Image> modSlots;
    [SerializeField] Image modDisplayPannel;
    [SerializeField] TextMeshProUGUI modDisplayDescriptionText;
    [SerializeField] TextMeshProUGUI modDisplayTitleText;

    [Header("Game - Datas")]
    [SerializeField] ResourceData resource1Data;
    [SerializeField] ResourceData resource2Data;
    [SerializeField] ResourceData resource3Data;
    [SerializeField] GameData gameData;

    [Header("Game Over - setup")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI endingTitle; 
    [SerializeField] TextMeshProUGUI gameOverPointsTotal; 
    [SerializeField] Image endingImage;
    [SerializeField] TextMeshProUGUI gameOverResource1QuantityText;
    [SerializeField] TextMeshProUGUI gameOverResource2QuantityText;
    [SerializeField] TextMeshProUGUI gameOverResource3QuantityText;

    [Header("Game Over - sprites")]
    [SerializeField] Sprite floraEndingSprite;
    [SerializeField] Sprite cultureEndingSprite;
    [SerializeField] Sprite faunaEndingSprite;
    [SerializeField] Sprite allEndingSprite;

    [Header("Game Over - Title strings")]
    [SerializeField] string floraEndingText;
    [SerializeField] string cultureEndingText;
    [SerializeField] string faunaEndingText;
    [SerializeField] string allEndingText;

    private void Awake()
    {
        EventBus.onResourceModifiedEvent += UpdateResources;
        EventBus.onTurnEndedEvent += UpdateGameUI;
        EventBus.onTurnInitializedEvent += UpdateGameUI;
        EventBus.onGameInitializedEvent += UpdateGameUI;
        EventBus.onGameOverRequestedEvent += SetupEndingScreen;
    }
    private void OnDestroy()
    {
        EventBus.onResourceModifiedEvent -= UpdateResources;
        EventBus.onTurnEndedEvent -= UpdateGameUI;
    }

    public void OnModIconClicked(int modIndex)
    {
        modDisplayTitleText.text = gameData.currentModifiers[modIndex].ModifierName;
        modDisplayDescriptionText.text = gameData.currentModifiers[modIndex].ModifierDescription;
        modDisplayPannel.gameObject.SetActive(true);
        EventBus.onTurnInitializedEvent -= UpdateGameUI;
        EventBus.onGameOverRequestedEvent -= SetupEndingScreen;
        EventBus.onGameInitializedEvent -= UpdateGameUI;
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

    private void SetupEndingScreen(Constants.EndType endType) 
    {
        gameOverScreen.SetActive(true);

        gameOverResource1QuantityText.text = resource1Data.ResourceQuantity.ToString();
        gameOverResource2QuantityText.text = resource2Data.ResourceQuantity.ToString();
        gameOverResource3QuantityText.text = resource3Data.ResourceQuantity.ToString();

        gameOverPointsTotal.text = "Total Points: " + gameData.TotalScore.ToString();
        switch (endType)
        {
            case Constants.EndType.FloraEnd:
                endingImage.sprite = floraEndingSprite;
                endingTitle.text = floraEndingText;
                break;
            case Constants.EndType.FaunaEnd:
                endingImage.sprite = faunaEndingSprite;
                endingTitle.text = faunaEndingText;
                break;
            case Constants.EndType.CultureEnd:
                endingImage.sprite = cultureEndingSprite;
                endingTitle.text = cultureEndingText;
                break;
            case Constants.EndType.AllEnd:
                endingImage.sprite = allEndingSprite;
                endingTitle.text = allEndingText;
                break;
            default:
                break;
        }
    }

    private void UpdateResources(ResourceData data) 
    {
        UpdateGameUI();
    }

    public void ReturnToMenu() 
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void PlayAgain() 
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
