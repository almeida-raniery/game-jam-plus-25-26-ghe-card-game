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

    public Constants.EndType EndType;

    [SerializeField] int resourceBeginQuantity = 3;

    public void ResetResource()
    {
        ResourceQuantity = resourceBeginQuantity;
        ResourceBaseValue = 10;
        ResourceScoreMultiplier = 1;
        ResourceTurnBonusPoints = 0;
    }

    public void ModifyResourceCount(int amount)
    {
        ResourceQuantity += amount;
        Debug.Log(ResourceName + ": " + ResourceQuantity.ToString() + "x");
        if (ResourceQuantity < 0)
            ResourceQuantity = 0;
        LastAmountAddedToResource = amount;
        EventBus.ResourceModifiedEvent(this);
    }

    public int CalculateTotalBonus()
    {
        int finalValue = (int)((ResourceBaseValue * ResourceQuantity + ResourceTurnBonusPoints) * ResourceScoreMultiplier);
        return finalValue;
    }
}
