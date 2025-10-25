using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = Constants.BASE_PATH + "New Resource")]
public class ResourceData : ScriptableObject
{
    public string ResourceName;
    public int ResourceQuantity { get; set; }
    public int ResourceBaseValue { get; set; }
    public float ResourceScoreMultiplier { get; set; }
    public int ResourceTurnBonusPoints { get; set; }
    public int LastAmountAddedToResource { get; set; }

    public void ResetResource()
    {
        ResourceQuantity = 0;
    }

    public void ModifyResourceCount(int amount)
    {
        ResourceQuantity = amount;
        if (ResourceQuantity < 0)
            ResourceQuantity = 0;
        LastAmountAddedToResource = amount;
        EventBus.ResourceModifiedEvent(this);
    }

    public int CalculateTotalBonus()
    {
        int finalValue = (int)((ResourceBaseValue + ResourceTurnBonusPoints) * ResourceScoreMultiplier);
        return finalValue;
    }
}
