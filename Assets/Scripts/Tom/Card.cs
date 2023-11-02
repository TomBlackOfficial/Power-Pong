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

    public void SetCardInformation(GameObject newModifier)
    {
        ModifierParent modifierScript = newModifier.GetComponent<ModifierParent>();

        ModifierParent.ModifierRarity rarity = modifierScript.rarity;
        Sprite icon = modifierScript.icon;
        string description = modifierScript.description;

        switch (rarity)
        {
            case ModifierParent.ModifierRarity.Common:
                cardSprite.sprite = CardSelection.instance.commonCardSprite;
                break;
            case ModifierParent.ModifierRarity.Rare:
                cardSprite.sprite = CardSelection.instance.rareCardSprite;
                break;
            case ModifierParent.ModifierRarity.Legendary:
                cardSprite.sprite = CardSelection.instance.legendaryCardSprite;
                break;
            case ModifierParent.ModifierRarity.Mystic:
                cardSprite.sprite = CardSelection.instance.mythicCardSprite;
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
