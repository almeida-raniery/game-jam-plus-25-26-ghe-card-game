using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GiveAction", menuName = Constants.BASE_CREATE_ACTIONS_PATH + "New Give Action")]

public class GiveCardAction : CardAction
{
    [Header("Resources")]
    [SerializeField]
    ResourceValueStruct[] resources;

    [Header("Others")]
    [SerializeField] int cardsToGain;
    [SerializeField] ModifierBase modifierToGain;

    public override void ExecuteAction()
    {
        foreach (ResourceValueStruct resval in resources)
        {
            resval.resource.ModifyResourceCount(resval.valueToGain);
        }

        if (modifierToGain != null)
            EventBus.GiveModifierEvent(modifierToGain);

        if (cardsToGain > 0)
            EventBus.AddCardsToDeckByNumberEvent(cardsToGain);
    }
}

[Serializable]
struct ResourceValueStruct
{
    public ResourceData resource;
    public int valueToGain;
}
