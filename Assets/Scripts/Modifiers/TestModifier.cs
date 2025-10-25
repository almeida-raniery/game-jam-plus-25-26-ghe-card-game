using UnityEngine;

[CreateAssetMenu(fileName ="TestModifier", menuName = Constants.BASE_CREATE_MODIFIER_PATH + "Test")]
public class TestModifier : ModifierBase
{
    [SerializeField] ResourceData resource;
    public override void Modify()
    {
        resource.ResourceScoreMultiplier += 1.25f;
    }
}
