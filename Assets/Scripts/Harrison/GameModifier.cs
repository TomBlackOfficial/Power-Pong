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
    public Paddle[] player = new Paddle[2];
    public float playerSpeedAdd = 0;
    public float playerSpeedMult = 1;
    public int playerHeightAdjustmentAdd = 0;
    public int playerHeightAdjustmentMult = 1;
    [Header("Ball Stats")]
    public List<Ball> ball = new List<Ball>();
    public float ballSpeedAdd = 0;
    public float ballSpeedMult = 1;
    public int ballSizeAdjustmentAdd = 0;
    public int ballSizeAdjustmentMult = 1;


    protected override void Start()
    {
        base.Start();
    }
}
