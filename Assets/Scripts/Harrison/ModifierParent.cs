using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class ModifierParent : MonoBehaviour
{
    public enum ModifierRarity
    {
        Common,
        Rare,
        Legendary,
        Mystic
    }
    [Header("Base Variable")]
    public ModifierRarity rarity;
    public string modifierName;
    public string description;
    public Image icon;
    public bool unique;
    protected virtual void Start()
    {
        
    }

    protected virtual void UpdateModifierComponents()
    {

    }
}
