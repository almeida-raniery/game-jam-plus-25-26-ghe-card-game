using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Setup
    [Header("Setup")]
    [SerializeField] private GameData gameData;
    [SerializeField] private List<CardData> AllNormalCardsList;
    [SerializeField] private List<CardData> AllModifierCardsList;
    [SerializeField] private List<ResourceData> levelResources;
    [SerializeField] private List<ModifierBase> initialModifiers;

    // Options
    [Header("Game Options")]
    [SerializeField] private int initialNormalCardsQuantity = 7;
    [SerializeField] private int initialModifierCardsQuantity = 3;
    [SerializeField] private bool allowForRepetitionInCards = true;
    [SerializeField] private int lastModifierPositionInDeck = 2;

    private System.Random rng;
    List<CardData> tempAllCardList;

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
        EventBus.onAddCardsToDeckByNumberEvent += AddMoreCardsToDeck;
    }

    private void UnsubscribeToEvents()
    {
        EventBus.onCardActionChoosenEvent -= HandleCardActionSelected;
        EventBus.onGiveModifierEvent -= GiveModifierToPlayer;
        EventBus.onAddCardsToDeckByNumberEvent -= AddMoreCardsToDeck;
    }

    private void InitializeGame()
    {
        var initialModifier = GetInitialModifier();

        gameData.ResetGameData();
        gameData.currentModifiers.Add(initialModifier);

        foreach (ResourceData resource in levelResources)
        {
            resource.ResetResource();
        }

        // We shuffle the cards and add to the deck
        CreateTheDeck();

        // We set the Deck
        foreach (var card in tempAllCardList)
        {
            gameData.gameCardPile.Enqueue(card);
        }

        if (!allowForRepetitionInCards)
            tempAllCardList.RemoveRange(0, initialNormalCardsQuantity + initialModifierCardsQuantity);

        EventBus.TakeCardFromDeckEvent(gameData.gameCardPile.Dequeue());
        EventBus.GameInitializedEvent();
    }

    public void AddMoreCardsToDeck(int quantityToAdd)
    {
        // We cant add more cards than it exists
        if (AllNormalCardsList.Count + AllModifierCardsList.Count < quantityToAdd)
            return;

        if (tempAllCardList.Count >= quantityToAdd)
        {
            for (int i = 0; i < quantityToAdd; i++)
            {
                // we add if the card is not a modifier card
                if (!tempAllCardList[quantityToAdd - 1].isModifierCard)
                {
                    gameData.gameCardPile.Enqueue(tempAllCardList[i]);
                }
                // if it is, we just take from the normal pile
                else
                {
                    gameData.gameCardPile.Enqueue(AllNormalCardsList[Random.Range(0, AllNormalCardsList.Count)]);
                }
            }
        }
        else
        {
            CreateTheDeck();
            AddMoreCardsToDeck(quantityToAdd); // TODO: TEST FOR RECURSION
        }
    }

    public void CreateTheDeck()
    {
        // We shuffle the cards and add to the deck
        List<CardData> tempNormalCardList = AllNormalCardsList;
        List<CardData> tempModCardList = AllModifierCardsList;
        tempAllCardList = new();
        rng = new System.Random();
        tempNormalCardList = tempNormalCardList.OrderBy(x => rng.Next()).ToList();
        tempModCardList = tempModCardList.OrderBy(x => rng.Next()).ToList();

        // We add the mod cards
        for (int j = 0; j < initialModifierCardsQuantity - 1; j++)
        {
            tempAllCardList.Add(tempModCardList[j]);
        }

        // We add the normal cards, minus the top one
        for (int i = 0; i < initialNormalCardsQuantity - lastModifierPositionInDeck; i++)
        {
            tempAllCardList.Add(tempNormalCardList[i]);
        }

        // We reshufle the pile
        tempAllCardList = tempAllCardList.OrderBy(x => rng.Next()).ToList();

        tempAllCardList.Add(tempModCardList[^1]);

        // We add the final cards, wich must always be normal
        for (int i = 1; i < lastModifierPositionInDeck + 1; i++)
        {

            tempAllCardList.Add(tempNormalCardList[^i]);
        }
    }

    // Entry point for selection
    public void HandleCardActionSelected(CardAction selectedAction)
    {
        selectedAction.ExecuteAction();
        foreach (var resource in levelResources)
        {
            resource.ResourceScoreMultiplier = 1;
        }
        HandleTurn();
    }

    public ModifierBase GetInitialModifier()
    {
        var rng = new System.Random();

        return initialModifiers.OrderBy(x => rng.Next()).ToList()[0];
    }

    public void HandleTurn()
    {
        // We iterate through all our modifiers
        foreach (ModifierBase modifier in gameData.currentModifiers)
        {
            modifier.Modify();
        }

        var pointsInTurnToAdd = 0;
        // We calculate the total score
        foreach (var resource in levelResources)
        {
            pointsInTurnToAdd += resource.CalculateTotalBonus();
            resource.ResourceTurnBonusPoints = 0;
        }

        gameData.TotalScore += pointsInTurnToAdd;

        PrepareNextTurn();
        EventBus.TurnEndedEvent();
    }

    public void PrepareNextTurn()
    {
        EventBus.TurnInitializedEvent();
        // If there are still cards on the pile
        if (gameData.gameCardPile.Count > 0)
            EventBus.TakeCardFromDeckEvent(gameData.gameCardPile.Dequeue());
        else
            print("Here");
            HandleGameOver();
    }

    public void GiveModifierToPlayer(ModifierBase modifierToGive)
    {
        gameData.currentModifiers.Add(modifierToGive);
        print("Gave Player modifier: " + modifierToGive.ModifierName);
    }

    public void RemoveRandomModifier()
    {
        if (gameData.currentModifiers.Count > 0)
        {
            var index = Random.Range(0, gameData.currentModifiers.Count);
            EventBus.onLoseModifierEvent(gameData.currentModifiers[index]);
            gameData.currentModifiers.RemoveAt(index);
        }
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
