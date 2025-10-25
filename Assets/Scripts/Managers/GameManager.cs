using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Setup
    [SerializeField] private GameData gameData;
    [SerializeField] private List<CardData> AllCardsList;
    [SerializeField] private List<ResourceData> levelResources;
    [SerializeField] private int initialDeckSize = 10;

    private CardAction turnSelectedAction;
    private System.Random rng;

    private void Awake()
    {
        SubscribeToEvents();
    }

    private void Start()
    {
        InitializeGame();
    }

    private void SubscribeToEvents()
    {
        EventBus.onCardActionChoosenEvent += HandleCardActionSelected;
        EventBus.onGiveModifierEvent += GiveModifierToPlayer;
    }

    private void UnsubscribeToEvents()
    {
        EventBus.onCardActionChoosenEvent -= HandleCardActionSelected;
        EventBus.onGiveModifierEvent -= GiveModifierToPlayer;
    }

    private void InitializeGame()
    {
        gameData.currentModifiers.Clear();
        gameData.gameCardPile.Clear();

        foreach (ResourceData resource in levelResources)
        {
            resource.ResetResource();
        }

        // We shuffle the cards and add to the deck
        List<CardData> tempCardList = AllCardsList;
        rng = new System.Random();
        tempCardList = tempCardList.OrderBy(x => rng.Next()).ToList();
        for (int i = 0; i < initialDeckSize; i++) 
        {
            gameData.gameCardPile.Enqueue(tempCardList[i]);
        }

        EventBus.TakeCardFromDeckEvent(gameData.gameCardPile.Dequeue());
    }

    // Entry point for selection
    public void HandleCardActionSelected(CardAction selectedAction)
    {
        selectedAction.ExecuteAction();

        PrepareNextTurn();
    }

    public void PrepareNextTurn()
    {
        EventBus.TakeCardFromDeckEvent(gameData.gameCardPile.Dequeue());
    }

    public void GiveModifierToPlayer(ModifierBase modifierToGive) 
    {
        gameData.currentModifiers.Add(modifierToGive);
        print("Gave Player modifier: " + modifierToGive.ModifierName);
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}
