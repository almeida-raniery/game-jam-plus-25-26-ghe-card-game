using UnityEngine;

public abstract class ModifierBase : ScriptableObject
{
    public string ModifierName;
    public string ModifierDescription;
    public Sprite icon;
    public abstract void Modify();
}
