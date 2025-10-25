using UnityEngine;

public abstract class CardAction : ScriptableObject
{
    public string CardActionName;
    public string CardActionDescription;

    public abstract void ExecuteAction();
}
