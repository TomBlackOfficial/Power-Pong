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
    public ModifierRarity rarity;
<<<<<<< Updated upstream
    public string modifierName;
    public string description;
    public Image icon;
=======
    protected virtual void Start()
    {
        
    }

    protected virtual void UpdateModifierComponents()
    {

    }
>>>>>>> Stashed changes
}
