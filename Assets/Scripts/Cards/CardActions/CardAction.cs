using UnityEngine;

public abstract class CardAction : ScriptableObject
{
    public string CardActionName;
    public string CardActionDescription;
    public GiveCardAction GiveCardAction;

    public abstract void ExecuteAction();
}
