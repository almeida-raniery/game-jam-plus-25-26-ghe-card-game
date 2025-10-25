using UnityEngine;

[CreateAssetMenu(fileName = "ResourceGainHalfModifier", menuName = Constants.BASE_CREATE_MODIFIER_PATH + "New Gain Half Modifier")]
public class ResourceGainHalfFromOtherModifier : ModifierBase
{
    [SerializeField] ResourceData resourceToAddTo;
    [SerializeField] ResourceData resourceToGainHalfFrom;

    public override void Modify()
    {
        resourceToAddTo.ResourceTurnBonusPoints += ((resourceToGainHalfFrom.ResourceBaseValue + resourceToGainHalfFrom.ResourceTurnBonusPoints) / 2);
    }
}
