using System;
using System.Collections.Generic;

public static class EventBus
{
    public static Action onGameOverRequestedEvent;
    public static void GameOverRequestedEvent()
    {
        onGameOverRequestedEvent?.Invoke();
    }

    public static Action onCardActionChoosenEvent;
    public static void CardActionChoosenEvent(CardAction choosenAction)
    {
        onCardActionChoosenEvent?.Invoke();
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

    public static Action<List<CardData>> onAddCardsToDeckEvent;
    public static void AddCardsToDeckEvent(List<CardData> cardsToAdd) 
    {
        onAddCardsToDeckEvent?.Invoke(cardsToAdd);
    }
}