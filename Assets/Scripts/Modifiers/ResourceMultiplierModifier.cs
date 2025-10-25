using UnityEngine;
[CreateAssetMenu(fileName = "ResourceMultiplierModifier", menuName = Constants.BASE_CREATE_MODIFIER_PATH + "Resource Multiplier Modifier")]
public class ResourceMultiplierModifier : ModifierBase
{
    [SerializeField] ResourceData resourceToMultiply;
    [SerializeField] float multiplierAmount;

    public override void Modify()
    {
        resourceToMultiply.ResourceQuantity = (int)(resourceToMultiply.ResourceQuantity * multiplierAmount);
    }
}
