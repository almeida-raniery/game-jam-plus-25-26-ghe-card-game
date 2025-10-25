using UnityEngine;

[CreateAssetMenu(fileName = "GiveModifierAction", menuName = Constants.BASE_CREATE_ACTIONS_PATH + "New Give Modifier Action")]
public class GiveModifierCardAction : CardAction
{
    [SerializeField]
    private ModifierBase modifierToGive;

    public override void ExecuteAction()
    {
        EventBus.GiveModifier(modifierToGive);
    }
}
