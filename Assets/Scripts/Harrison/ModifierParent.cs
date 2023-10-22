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

    public virtual void StartModifierEffect()
    {
        //paddleList.Add(this);
    }
    /*
    GameObject GO = Instantiate(Prefab, parent.tranform);
    GO.transform.parent = parent.transform;
    GO.GetComponent<ModifierParent>().StartModifierEffect();
    list.Add(GO);
    */
}
