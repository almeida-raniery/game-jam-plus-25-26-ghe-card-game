using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Setup
    [Header("Setup")]
    [SerializeField] private GameData gameData;
    [SerializeField] private List<CardData> AllCardsList;
    [SerializeField] private List<ResourceData> levelResources;

    // Options
    [Header("Game Options")]
    [SerializeField] private int initialDeckSize = 10;
    [SerializeField] private bool allowForRepetitioninCards = true;

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
        if (!allowForRepetitioninCards)
            tempCardList.RemoveRange(0, initialDeckSize);

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
        // If there are still cards on the pile
        if (gameData.gameCardPile.Count > 0)
            EventBus.TakeCardFromDeckEvent(gameData.gameCardPile.Dequeue());
        else
            HandleGameOver();
    }

    public void GiveModifierToPlayer(ModifierBase modifierToGive)
    {
        gameData.currentModifiers.Add(modifierToGive);
        print("Gave Player modifier: " + modifierToGive.ModifierName);
    }

    public void HandleGameOver()
    {
        EventBus.GameOverRequestedEvent();
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}
