using UnityEngine;

[CreateAssetMenu(fileName = "NewCardData", menuName = Constants.BASE_CREATE_DATA_PATH + "New Card Data")]
public class CardData : ScriptableObject
{
    public string cardTitle;
    public string cardText;
    public Sprite cardImage;
    public AudioClip cardSound;

    public CardAction cardLeftAction;
    public CardAction cardRightAction;
}
