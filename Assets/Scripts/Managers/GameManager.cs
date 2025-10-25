using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<CardData> AllCardsList;

    public List<ModifierBase> currentModifiers = new List<ModifierBase>();
    public Queue<CardData> gameCardPile = new Queue<CardData>();

    [SerializeField]
    GameData gameData;

    private List<ResourceData> levelResources;

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
    }

    private void UnsubscribeToEvents()
    {
    }

    private void InitializeGame() 
    {
        currentModifiers.Clear();

        foreach (ResourceData resource in levelResources)
        {
            resource.ResetResource();
        }
    }

    public void ExecuteTurn() 
    {
        
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}
