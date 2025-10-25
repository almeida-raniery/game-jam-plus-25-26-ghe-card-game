using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = Constants.BASE_PATH + "New Resource")]
public class ResourceData : ScriptableObject
{
    public string ResourceName;
    public int ResourceQuantity { get; set; }

    public void ResetResource() 
    {
        ResourceQuantity = 0;
    }
}
