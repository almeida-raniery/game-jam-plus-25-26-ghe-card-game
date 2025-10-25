using UnityEngine;

[CreateAssetMenu(fileName = "AddToResourceAction", menuName = Constants.BASE_CREATE_ACTIONS_PATH + "New Add To Resource Action")]
public class AddToResourceAction : CardAction
{
    [SerializeField] ResourceData resourceToModify;
    [SerializeField] int QuantityToAdd;

    public override void ExecuteAction()
    {
        int value = resourceToModify.ResourceQuantity + QuantityToAdd;
        resourceToModify.ModifyResourceCount(value);
    }
}
