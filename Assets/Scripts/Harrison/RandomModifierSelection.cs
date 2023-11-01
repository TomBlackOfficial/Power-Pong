using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModifierSelection : MonoBehaviour
{
    public static RandomModifierSelection instance;

    public enum PlayerWhoSelected
    {
        P1,
        P2,
        Both
    }

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
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
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
        GameManager manager = GameManager.instance.GetComponent<GameManager>();
        List<GameObject> cards = new List<GameObject>();
        GameObject tempCard;
        List<GameObject> cardsTried = new List<GameObject>();
        bool validCard = true;
        bool cardAdded = false;
        for (int c = 0; c < numberOfCards; c++)
        {
            cardAdded = false;
            while (!cardAdded)
            {
                switch (SelectRarity())
                {
                    case ModifierParent.ModifierRarity.Common:
                        do 
                        {
                            tempCard = modifierList.common[Random.Range(0, modifierList.common.Count)];
                            validCard = true;
                            if (cardsTried.Contains(tempCard))
                            {
                                validCard = false;
                                if (cardsTried.Count >= modifierList.common.Count)
                                {
                                    break;
                                }
                                continue;
                            }
                            else
                            {
                                cardsTried.Add(tempCard);
                            }
                            if (cards.Contains(tempCard))
                            {
                                validCard = false;
                                continue;
                            }
                            if (tempCard.GetComponent<ModifierParent>().unique != ModifierParent.Unique.NotUnique)
                            {
                                if (tempCard.GetComponent<ModifierParent>().unique == ModifierParent.Unique.ChoosingPlayer)
                                {
                                    if (manager.pickedModifiers.ContainsKey(tempCard.name))
                                    {
                                        if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.Both)
                                        {
                                            validCard = false;
                                        }
                                        else if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.P1 && manager.loser.isPlayer1)
                                        {
                                            validCard = false;
                                        }
                                        else if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.P2 && !manager.loser.isPlayer1)
                                        {
                                            validCard = false;
                                        }
                                    }
                                }
                                else if (tempCard.GetComponent<ModifierParent>().unique == ModifierParent.Unique.ForWholeGame)
                                {
                                    if (manager.pickedModifiers.ContainsKey(tempCard.name))
                                    {
                                        validCard = false;
                                    }
                                }
                            }
                        } while (!validCard);
                        cards.Add(tempCard);
                        cardAdded = true;
                        break;
                    case ModifierParent.ModifierRarity.Rare:
                        do
                        {
                            tempCard = modifierList.rare[Random.Range(0, modifierList.rare.Count)];
                            validCard = true;
                            if (cardsTried.Contains(tempCard))
                            {
                                if (cardsTried.Count >= modifierList.rare.Count)
                                {
                                    break;
                                }
                                validCard = false;
                                continue;
                            }
                            else
                            {
                                cardsTried.Add(tempCard);
                            }
                            if (cards.Contains(tempCard))
                            {
                                validCard = false;
                                continue;
                            }
                            if (tempCard.GetComponent<ModifierParent>().unique != ModifierParent.Unique.NotUnique)
                            {
                                if (tempCard.GetComponent<ModifierParent>().unique == ModifierParent.Unique.ChoosingPlayer)
                                {
                                    if (manager.pickedModifiers.ContainsKey(tempCard.name))
                                    {
                                        if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.Both)
                                        {
                                            validCard = false;
                                        }
                                        else if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.P1 && manager.loser.isPlayer1)
                                        {
                                            validCard = false;
                                        }
                                        else if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.P2 && !manager.loser.isPlayer1)
                                        {
                                            validCard = false;
                                        }
                                    }
                                }
                                else if (tempCard.GetComponent<ModifierParent>().unique == ModifierParent.Unique.ForWholeGame)
                                {
                                    if (manager.pickedModifiers.ContainsKey(tempCard.name))
                                    {
                                        validCard = false;
                                    }
                                }
                            }
                        } while (!validCard);
                        cards.Add(tempCard);
                        cardAdded = true;
                        break;
                    case ModifierParent.ModifierRarity.Legendary:
                        do
                        {
                            tempCard = modifierList.legendary[Random.Range(0, modifierList.legendary.Count)];
                            validCard = true;
                            if (cardsTried.Contains(tempCard))
                            {
                                if (cardsTried.Count >= modifierList.legendary.Count)
                                {
                                    break;
                                }
                                validCard = false;
                                continue;
                            }
                            else
                            {
                                cardsTried.Add(tempCard);
                            }
                            if (cards.Contains(tempCard))
                            {
                                validCard = false;
                                continue;
                            }
                            if (tempCard.GetComponent<ModifierParent>().unique != ModifierParent.Unique.NotUnique)
                            {
                                if (tempCard.GetComponent<ModifierParent>().unique == ModifierParent.Unique.ChoosingPlayer)
                                {
                                    if (manager.pickedModifiers.ContainsKey(tempCard.name))
                                    {
                                        if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.Both)
                                        {
                                            validCard = false;
                                        }
                                        else if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.P1 && manager.loser.isPlayer1)
                                        {
                                            validCard = false;
                                        }
                                        else if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.P2 && !manager.loser.isPlayer1)
                                        {
                                            validCard = false;
                                        }
                                    }
                                }
                                else if (tempCard.GetComponent<ModifierParent>().unique == ModifierParent.Unique.ForWholeGame)
                                {
                                    if (manager.pickedModifiers.ContainsKey(tempCard.name))
                                    {
                                        validCard = false;
                                    }
                                }
                            }
                        } while (!validCard);
                        cards.Add(tempCard);
                        cardAdded = true;
                        break;
                    case ModifierParent.ModifierRarity.Mystic:
                        do
                        {
                            tempCard = modifierList.mystic[Random.Range(0, modifierList.mystic.Count)];
                            validCard = true;
                            if (cardsTried.Contains(tempCard))
                            {
                                if (cardsTried.Count >= modifierList.mystic.Count)
                                {
                                    break;
                                }
                                validCard = false;
                                continue;
                            }
                            else
                            {
                                cardsTried.Add(tempCard);
                            }
                            if (cards.Contains(tempCard))
                            {
                                validCard = false;
                                continue;
                            }
                            if (tempCard.GetComponent<ModifierParent>().unique != ModifierParent.Unique.NotUnique)
                            {
                                if (tempCard.GetComponent<ModifierParent>().unique == ModifierParent.Unique.ChoosingPlayer)
                                {
                                    if (manager.pickedModifiers.ContainsKey(tempCard.name))
                                    {
                                        if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.Both)
                                        {
                                            validCard = false;
                                        }
                                        else if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.P1 && manager.loser.isPlayer1)
                                        {
                                            validCard = false;
                                        }
                                        else if (manager.pickedModifiers[tempCard.name] == PlayerWhoSelected.P2 && !manager.loser.isPlayer1)
                                        {
                                            validCard = false;
                                        }
                                    }
                                }
                                else if (tempCard.GetComponent<ModifierParent>().unique == ModifierParent.Unique.ForWholeGame)
                                {
                                    if (manager.pickedModifiers.ContainsKey(tempCard.name))
                                    {
                                        validCard = false;
                                    }
                                }
                            }
                        } while (!validCard);
                        cards.Add(tempCard);
                        cardAdded = true;
                        break;
                }
            }
        }
        return cards;
    }
    private ModifierParent.ModifierRarity SelectRarity()
    {
        if (Random.Range(0f, 100f) < mysticChance)
        {
            return ModifierParent.ModifierRarity.Mystic;
        }
        if (Random.Range(0f, 100f) < legendaryChance)
        {
            return ModifierParent.ModifierRarity.Legendary;
        }
        if (Random.Range(0f, 100f) < rareChance)
        {
            return ModifierParent.ModifierRarity.Rare;
        }
        return ModifierParent.ModifierRarity.Common;
    }
    
}
