using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModifierSelection : MonoBehaviour
{
    public static RandomModifierSelection instance;

    [SerializeField] private List<GameObject> masterList = new List<GameObject>();
    [SerializeField] [Range(0, 100)] private float rareChance = 20;
    [SerializeField] [Range(0, 100)] private float legendaryChance = 5;
    [SerializeField] [Range(0, 100)] private float mysticChance = 1;
    public struct Modifiers
    {
        public List<GameObject> common;
        public List<GameObject> rare;
        public List<GameObject> legendary;
        public List<GameObject> mystic;
    }
    private Modifiers modifierList;
    private void Awake()
    {
        instance = this;

        modifierList.common = new List<GameObject>();
        modifierList.rare = new List<GameObject>();
        modifierList.legendary = new List<GameObject>();
        modifierList.mystic = new List<GameObject>();
        for (int m = 0; m < masterList.Count; m++)
        {
            ModifierParent MPOut = null;
            if (!masterList[m].TryGetComponent<ModifierParent>(out MPOut))
            {
                continue;
            }
            switch (MPOut.rarity)
            {
                case ModifierParent.ModifierRarity.Common:
                    modifierList.common.Add(masterList[m]);
                    break;
                case ModifierParent.ModifierRarity.Rare:
                    modifierList.rare.Add(masterList[m]);
                    break;
                case ModifierParent.ModifierRarity.Legendary:
                    modifierList.legendary.Add(masterList[m]);
                    break;
                case ModifierParent.ModifierRarity.Mystic:
                    modifierList.mystic.Add(masterList[m]);
                    break;
            }
        }
    }
    public List<GameObject> SelectedCards(int numberOfCards = 2)
    {
        List<GameObject> cards = new List<GameObject>();
        GameObject tempCard;
        for (int c = 0; c < numberOfCards; c++)
        {
            switch (SelectRarity())
            {
                case ModifierParent.ModifierRarity.Common:
                    do 
                    {
                        tempCard = modifierList.common[Random.Range(0, modifierList.common.Count)];
                    } while (cards.Contains(tempCard));
                    cards.Add(tempCard);
                    break;
                case ModifierParent.ModifierRarity.Rare:
                    do 
                    {
                        tempCard = modifierList.rare[Random.Range(0, modifierList.rare.Count)];
                    } while (cards.Contains(tempCard));
                    cards.Add(tempCard);
                    break;
                case ModifierParent.ModifierRarity.Legendary:
                    do 
                    {
                        tempCard = modifierList.legendary[Random.Range(0, modifierList.legendary.Count)];
                    } while (cards.Contains(tempCard));
                    cards.Add(tempCard);
                    break;
                case ModifierParent.ModifierRarity.Mystic:
                    do
                    {
                        tempCard = modifierList.mystic[Random.Range(0, modifierList.mystic.Count)];
                    } while (cards.Contains(tempCard));
                    cards.Add(tempCard);
                    break;
            }
        }
        return cards;
    }
    private ModifierParent.ModifierRarity SelectRarity()
    {
        if (Random.Range(0, 100) < mysticChance)
        {
            return ModifierParent.ModifierRarity.Mystic;
        }
        if (Random.Range(0, 100) < legendaryChance)
        {
            return ModifierParent.ModifierRarity.Legendary;
        }
        if (Random.Range(0, 100) < rareChance)
        {
            return ModifierParent.ModifierRarity.Rare;
        }
        return ModifierParent.ModifierRarity.Common;
    }
    
}
