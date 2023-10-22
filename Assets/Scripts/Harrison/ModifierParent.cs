using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class ModifierParent : ScriptableObject
{
    public enum ModifierRarity
    {
        Common,
        Rare,
        Legendary,
        Mystic
    }
    public ModifierRarity rarity;
    public string modifierName;
    public string description;
    public Image icon;
    protected virtual void Start()
    {
        
    }

    protected virtual void UpdateModifierComponents()
    {

    }
}
