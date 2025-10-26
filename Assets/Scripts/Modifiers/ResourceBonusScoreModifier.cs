using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceBonusScoreMultiplierModifier", menuName = Constants.BASE_CREATE_MODIFIER_PATH + "New Bonus Score Multiplier Modifier")]
public class ResourceBonusScoreModifier : ModifierBase
{
    [SerializeField] List<ResourceData> resourceList;
    [SerializeField] float multiplierAmount;

    public override void Modify()
    {
        foreach (ResourceData resource in resourceList)
        {
            if (resource.ResourceScoreMultiplier > 1)
                resource.ResourceScoreMultiplier += multiplierAmount;
            else
                resource.ResourceScoreMultiplier = multiplierAmount;
        }
    }
}
