using UnityEngine;

public abstract class ModifierBase : ScriptableObject
{
    public string ModifierName;
    public string ModifierDescription;
    public abstract void Modify();
}
