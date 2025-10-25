using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPresenter : MonoBehaviour
{
    // Setup
    [SerializeField] TextMeshProUGUI cardTitleText;
    [SerializeField] TextMeshProUGUI cardDescriptionText;
    [SerializeField] Image cardImage;
    [SerializeField] TextMeshProUGUI cardLeftActionText;
    [SerializeField] TextMeshProUGUI cardRightActionText;
    
    CardAction cardLeftAction;
    CardAction cardRightAction;

    private void Awake()
    {
        EventBus.onTakeCardFromDeckEvent += SetCard;
    }

    private void OnDestroy()
    {
        EventBus.onTakeCardFromDeckEvent -= SetCard;
    }

    public void SetCard(CardData cardData)
    {
        cardTitleText.text = cardData.cardTitle;
        cardDescriptionText.text = cardData.cardText;
        cardImage.sprite = cardData.cardImage;
        cardLeftAction = cardData.cardLeftAction;
        cardLeftActionText.text = cardData.cardLeftAction.CardActionName;
        cardRightAction = cardData.cardRightAction;
        cardRightActionText.text = cardData.cardRightAction.CardActionName;
    }

    public void LeftActionClicked() 
    {
        EventBus.CardActionChoosenEvent(cardLeftAction);
    }

    public void RightActionClicked() 
    {
        EventBus.CardActionChoosenEvent(cardRightAction);
    }
}
