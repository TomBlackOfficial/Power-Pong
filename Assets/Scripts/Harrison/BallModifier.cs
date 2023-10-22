using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallModifier : ModifierParent
{
    [Header("Ball Stats")]
    public Ball myBall;
    public float ballSpeedAdd = 0;
    public float ballSpeedMult = 1;
    public int ballSizeAdjustmentAdd = 0;
    public int ballSizeAdjustmentMult = 1;
    protected override void Start()
    {
        base.Start();
    }

    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        myBall = GetComponentInParent<Ball>();
    }
}
