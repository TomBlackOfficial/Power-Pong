using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection : MonoBehaviour
{
    public static CardSelection instance;

    List<GameObject> modifiers = new List<GameObject>();

    public Card[] cards;

    public Sprite commonCardSprite;
    public Sprite rareCardSprite;
    public Sprite legendaryCardSprite;
    public Sprite mythicCardSprite;

    private void Awake()
    {
        instance = this;
    }

    public void GetModifiers()
    {
        GetComponent<CustomEventSystem>().StartEventSystem();
        modifiers = RandomModifierSelection.instance.SelectedCards();
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetCardInformation(modifiers[i]);
        }
    }
}
