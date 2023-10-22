using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleModifier : ModifierParent
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
    public int playerHeightAdjustmentMult = 1;

    protected override void Start()
    {
        base.Start();
    }
}
