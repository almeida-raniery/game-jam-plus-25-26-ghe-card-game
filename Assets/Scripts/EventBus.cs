using System;
using UnityEngine;

public static class EventBus
{
    public static Action onGameOverRequestedEvent;
    public static Action onCardActionChoosenEvent;
    public static Action<ModifierBase> onGiveModifierEvent;

    public static void GameOverRequestedEvent() 
    {
        onGameOverRequestedEvent?.Invoke();
    }

    public static void CardActionChoosen(CardAction choosenAction) 
    {
        onCardActionChoosenEvent?.Invoke();
    }

    public static void GiveModifier(ModifierBase modifier) 
    {
        onGiveModifierEvent?.Invoke(modifier);
    }
}