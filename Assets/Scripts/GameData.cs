using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = Constants.BASE_CREATE_DATA_PATH + "New Game Data")]
public class GameData : ScriptableObject
{
    // Game Logic Vars
    [HideInInspector] public List<ModifierBase> currentModifiers = new List<ModifierBase>();
    public Queue<CardData> gameCardPile = new Queue<CardData>();
    public int TotalScore = 0;

    public void ResetGameData() 
    {
        currentModifiers.Clear();
        gameCardPile.Clear();
        TotalScore = 0;
    }
}
