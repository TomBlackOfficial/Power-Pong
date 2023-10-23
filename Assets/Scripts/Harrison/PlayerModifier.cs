using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifier : ModifierParent
{
    public enum PlayerToAffect
    {
        ChoosingPlayer,
        OposingPlayer,
        BothPlayers
    }
    [Header("Player Stats")]
    public PlayerToAffect playerToAffect;
    public Paddle player;
    public float playerSpeedAdd = 0;
    public float playerSpeedMult = 1;
    public int playerHeightAdjustmentAdd = 0;
    public float playerHeightAdjustmentMult = 1;

    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        player = GetComponentInParent<Paddle>();
    }
}
