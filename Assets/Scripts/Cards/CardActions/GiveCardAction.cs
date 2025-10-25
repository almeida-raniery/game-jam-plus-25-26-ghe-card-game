using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GiveAction", menuName = Constants.BASE_CREATE_ACTIONS_PATH + "New Give Action")]

public class GiveCardAction : CardAction
{
    [SerializeField] GameData gameData;

    [Header("Resources")]
    [SerializeField]
    ResourceValueStruct[] resources;

    [Header("Others")]
    [SerializeField] int cardsToGain;
    [SerializeField] ModifierBase modifierToGain;
    [SerializeField] bool removeRandomModifier;

    [Header("Spaghetti")]
    [SerializeField] ModifierBase troyMod;
    [SerializeField] ResourceData culture;
    [SerializeField] ModifierBase WindsMod;
    public override void ExecuteAction()
    {
        foreach (ResourceValueStruct resval in resources)
        {
            int valueToGain = resval.valueToGain;
            if (gameData.currentModifiers.Contains(troyMod) && resval.resource == culture && valueToGain < 0)
                valueToGain += 1;

            resval.resource.ModifyResourceCount(valueToGain);
        }

        if (modifierToGain != null)
            EventBus.GiveModifierEvent(modifierToGain);

        if (cardsToGain > 0)
        {
            int cardsToGainNumber = cardsToGain;
            if (gameData.currentModifiers.Contains(WindsMod))
                cardsToGainNumber += 1;
            EventBus.AddCardsToDeckByNumberEvent(cardsToGainNumber);
        }

        if (removeRandomModifier && gameData.currentModifiers.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, gameData.currentModifiers.Count);
            EventBus.LoseModifierEvent(gameData.currentModifiers[index]);
            gameData.currentModifiers.RemoveAt(index);
        }
    }
}

[Serializable]
struct ResourceValueStruct
{
    public ResourceData resource;
    public int valueToGain;
}
