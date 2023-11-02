using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Card : MonoBehaviour
{
    public GameObject modifier;
    public SpriteRenderer cardSprite;
    public SpriteRenderer iconSprite;
    public TextMesh descriptionText;
    [SerializeField] private CustomButton myButton;

    public void SetCardInformation(GameObject newModifier)
    {
        ModifierParent modifierScript = newModifier.GetComponent<ModifierParent>();
        if (myButton == null)
        {
            myButton.GetComponent<CustomButton>();
        }

        ModifierParent.ModifierRarity rarity = modifierScript.rarity;
        Sprite icon = modifierScript.icon;
        string description = modifierScript.description;

        switch (rarity)
        {
            case ModifierParent.ModifierRarity.Common:
                cardSprite.sprite = CardSelection.instance.commonCardSprite;
                myButton.highlightedColor = GameManager.instance.commonColour;
                break;
            case ModifierParent.ModifierRarity.Rare:
                cardSprite.sprite = CardSelection.instance.rareCardSprite;
                myButton.highlightedColor = GameManager.instance.rareColour;
                break;
            case ModifierParent.ModifierRarity.Legendary:
                cardSprite.sprite = CardSelection.instance.legendaryCardSprite;
                myButton.highlightedColor = GameManager.instance.legendaryColour;
                break;
            case ModifierParent.ModifierRarity.Mystic:
                cardSprite.sprite = CardSelection.instance.mythicCardSprite;
                myButton.highlightedColor = GameManager.instance.mysticColour;
                break;
        }

        modifier = newModifier;
        iconSprite.sprite = icon;
        descriptionText.text = description.Replace("\\n", "\n").ToUpper();
    }

    public void SelectModifier()
    {
        GameManager.instance.ApplyModifier(modifier, this.transform.position);
    }
}
