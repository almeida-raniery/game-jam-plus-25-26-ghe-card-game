using UnityEngine;

[CreateAssetMenu(fileName = "NewCardData", menuName = Constants.BASE_CREATE_DATA_PATH + "New Card Data")]
public class CardData : ScriptableObject
{
    public bool isModifierCard;
    public string cardTitle;
    public string cardText;
    public Sprite cardImage;
    public Sprite cardBackground;
    public AudioClip cardSound;

    public CardAction cardLeftAction;
    public CardAction cardRightAction;
}
