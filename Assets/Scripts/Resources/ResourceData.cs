using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = Constants.BASE_PATH + "New Resource")]
public class ResourceData : ScriptableObject
{
    public string ResourceName;
    public int ResourceQuantity { get; set; }

    public void ResetResource()
    {
        ResourceQuantity = 0;
    }

    public void ModifyResourceCount(int amount)
    {
        ResourceQuantity = amount;
        if (ResourceQuantity < 0)
            ResourceQuantity = 0;
        EventBus.ResourceModifiedEvent(this);
    }
}
