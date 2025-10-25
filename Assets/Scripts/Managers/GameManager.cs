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
    }

    private void UnsubscribeToEvents()
    {
        EventBus.onCardActionChoosenEvent -= HandleCardActionSelected;
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
    }

    // Entry point for selection
    public void HandleCardActionSelected(CardAction selectedAction)
    {
        selectedAction.ExecuteAction();

        PrepareNextTurn();
    }

    public void PrepareNextTurn()
    {

    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}
