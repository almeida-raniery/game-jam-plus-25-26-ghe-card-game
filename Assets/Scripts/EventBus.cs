using System;
using System.Collections.Generic;

public static class EventBus
{
    public static Action onGameInitializedEvent;
    public static void GameInitializedEvent()
    {
        onGameInitializedEvent?.Invoke();
    }
    public static Action onGameOverRequestedEvent;
    public static void GameOverRequestedEvent()
    {
        onGameOverRequestedEvent?.Invoke();
    }

    public static Action<CardAction> onCardActionChoosenEvent;
    public static void CardActionChoosenEvent(CardAction choosenAction)
    {
        onCardActionChoosenEvent?.Invoke(choosenAction);
    }

    public static Action<ModifierBase> onGiveModifierEvent;
    public static void GiveModifierEvent(ModifierBase modifier)
    {
        onGiveModifierEvent?.Invoke(modifier);
    }

    public static Action<CardData> onTakeCardFromDeckEvent;
    public static void TakeCardFromDeckEvent(CardData cardData)
    {
        onTakeCardFromDeckEvent?.Invoke(cardData);
    }

    public static Action<List<CardData>> onSpecificAddCardsToDeckEvent;
    public static void AddSpecificCardsToDeckEvent(List<CardData> cardsToAdd)
    {
        onSpecificAddCardsToDeckEvent?.Invoke(cardsToAdd);
    }

    public static Action<int> onAddCardsToDeckByNumberEvent;
    public static void AddCardsToDeckByNumberEvent(int cardsToAdd)
    {
        onAddCardsToDeckByNumberEvent?.Invoke(cardsToAdd);
    }

    public static Action<ResourceData> onResourceModifiedEvent;
    public static void ResourceModifiedEvent(ResourceData resourceData)
    {
        onResourceModifiedEvent?.Invoke(resourceData);
    }

    public static Action<ModifierBase> onLoseModifierEvent;
    public static void LoseModifierEvent(ModifierBase mod)
    {
        onLoseModifierEvent?.Invoke(mod);
    }

    public static Action onTurnInitializedEvent;
    public static void TurnInitializedEvent()
    {
        onTurnInitializedEvent?.Invoke();
    }

    public static Action onTurnEndedEvent;
    public static void TurnEndedEvent()
    {
        onTurnEndedEvent?.Invoke();
    }
}