using UnityEngine;

[CreateAssetMenu(fileName = "ResourceGainMultiplierModifier", menuName = Constants.BASE_CREATE_MODIFIER_PATH + "New Resource Gain Multiplier Modifier")]
public class ResourceGainMultiplierModifier : ModifierBase
{
    [SerializeField] ResourceData resource;
    [SerializeField] float multiplierAmount;

    public override void Modify()
    {
        resource.ModifyResourceCount((int)(resource.LastAmountAddedToResource * multiplierAmount));
    }
}
