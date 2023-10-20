using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierParent : MonoBehaviour
{
    public enum ModifierRarity
    {
        Common,
        Rare,
        Legendary,
        Mystic
    }

    public ModifierRarity rarity;
}
