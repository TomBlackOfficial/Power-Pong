using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModifier : ModifierParent
{
    public enum PlayerToAffect
    {
        ChoosingPlayer,
        OposingPlayer,
        BothPlayers
    }
    [Header("Game Stats")]
    public GameManager manager;
    public PlayerToAffect playerToAffect;
    [Header("Player Stats")]
    public GameObject playerModifier;
    [Header("Ball Stats")]
    public GameObject ballModifier;

    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        manager = GetComponentInParent<GameManager>();
    }
}
