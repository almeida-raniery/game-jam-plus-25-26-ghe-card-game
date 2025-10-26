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

    [Header("Left Action")]
    public TextMeshProUGUI leftActionFloraGainText;
    public TextMeshProUGUI leftActionFaunaGainText;
    public TextMeshProUGUI leftActionCultureGainText;
    public TextMeshProUGUI leftModifierGainText;
    public TextMeshProUGUI leftCardGainText;

    [Header("Right Action")]
    public TextMeshProUGUI rightActionFloraGainText;
    public TextMeshProUGUI rightActionFaunaGainText;
    public TextMeshProUGUI rightActionCultureGainText;
    public TextMeshProUGUI rightModifierGainText;
    public TextMeshProUGUI rightCardGainText;

    CardAction cardLeftAction;
    CardAction cardRightAction;

    [SerializeField] ResourceData faunaResource;
    [SerializeField] ResourceData floraResource;
    [SerializeField] ResourceData cultureResource;

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

        // set the left resources gain
        if (cardLeftAction.GiveCardAction != null)
        {
            foreach (ResourceValueStruct resval in cardLeftAction.GiveCardAction.resources)
            {
                if (resval.resource == faunaResource)
                {
                    leftActionFaunaGainText.text = getAddSymbol(resval.valueToGain);
                }
                else if (resval.resource == floraResource)
                {
                    leftActionFloraGainText.text = getAddSymbol(resval.valueToGain);
                }
                else if (resval.resource == cultureResource)
                {
                    leftActionCultureGainText.text = getAddSymbol(resval.valueToGain);
                }
            }

            leftModifierGainText.text = "";

            if (cardLeftAction.GiveCardAction.modifierToGain) 
            {
                leftModifierGainText.text = "+ Mod";
            }
            if (cardLeftAction.GiveCardAction.removeRandomModifier)
            {
                leftModifierGainText.text = "- Mod";
            }
            leftCardGainText.text = "";
            if (cardLeftAction.GiveCardAction.cardsToGain > 0)
            {
                leftCardGainText.text = "+ " + cardLeftAction.GiveCardAction.cardsToGain + " cards";
            }
        }

        // set the left resources gain
        if (cardRightAction.GiveCardAction != null)
        {
            foreach (ResourceValueStruct resval in cardRightAction.GiveCardAction.resources)
            {
                if (resval.resource == faunaResource)
                {
                    rightActionFaunaGainText.text = getAddSymbol(resval.valueToGain);
                }
                else if (resval.resource == floraResource)
                {
                    rightActionFloraGainText.text = getAddSymbol(resval.valueToGain);
                }
                else if (resval.resource == cultureResource)
                {
                    rightActionCultureGainText.text = getAddSymbol(resval.valueToGain);
                }
            }

            rightModifierGainText.text = "";

            if (cardRightAction.GiveCardAction.modifierToGain)
            {
                rightModifierGainText.text = "+ Mod";
            }
            if (cardRightAction.GiveCardAction.removeRandomModifier)
            {
                rightModifierGainText.text = "- Mod";
            }
            rightCardGainText.text = "";
            if (cardRightAction.GiveCardAction.cardsToGain > 0)
            {
                rightCardGainText.text = "+ " + cardRightAction.GiveCardAction.cardsToGain + " cards";
            }
        }

        // set the right resources gain
    }

    private string getAddSymbol(int quant)
    {
        string returnSymbol = string.Empty;

        if (quant == 1)
        {
            returnSymbol = "+";
        }
        else if (quant == 2)
        {
            returnSymbol = "++";
        }
        else if (quant >= 3)
        {
            returnSymbol = "+++";
        }
        else if (quant == -1)
        {
            returnSymbol = "-";
        }
        else if (quant == -2)
        {
            returnSymbol = "--";
        }
        else if (quant <= -3)
        {
            returnSymbol = "---";
        }
        return returnSymbol;
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
