using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GiveAction", menuName = Constants.BASE_CREATE_ACTIONS_PATH + "New Give Action")]

public class GiveCardAction : CardAction
{
    [SerializeField] GameData gameData;

    [Header("Resources")]
    public ResourceValueStruct[] resources;

    [Header("Others")]
    public int cardsToGain;
    public ModifierBase modifierToGain;
    public bool removeRandomModifier;

    [Header("Spaghetti")]
    public ModifierBase troyMod;
    public ResourceData culture;
    public ModifierBase WindsMod;
    public override void ExecuteAction()
    {
        foreach (ResourceValueStruct resval in resources)
        {
            int valueToGain = resval.valueToGain;
            if (gameData.currentModifiers.Contains(troyMod) && resval.resource == culture && valueToGain < 0)
                valueToGain += 1;

            resval.resource.ModifyResourceCount(valueToGain);
        }

        if (modifierToGain != null && gameData.currentModifiers.Count < 4)
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
public struct ResourceValueStruct
{
    public ResourceData resource;
    public int valueToGain;
}