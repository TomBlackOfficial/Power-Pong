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
    public enum Unique
    {
        NotUnique,
        ChoosingPlayer,
        ForWholeGame
    }
    [Header("Base Variable")]
    public ModifierRarity rarity;
    public string modifierName;
    public string description;
    public Sprite icon;
    public Unique unique;
    public bool activateable;

    public virtual void ActivateAbility()
    {

    }
    public virtual void DeactivateAbility()
    {

    }
    public virtual void StartModifierEffect()
    {
        //paddleList.Add(this);
    }

    public virtual void InitializeValues()
    {

    }
    /*
    GameObject GO = Instantiate(Prefab, parent.tranform);
    GO.transform.parent = parent.transform;
    GO.GetComponent<ModifierParent>().StartModifierEffect();
    list.Add(GO);
    */
}
