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
    public float ballSizeAdjustmentMult = 1;
    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        myBall = GetComponentInParent<Ball>();
        float newSpeed = (myBall.speed + ballSpeedAdd) * ballSpeedMult;
        myBall.SetBallSpeed(newSpeed);
        //int newSize = (int)((float)(myBall.size + ballSizeAdjustmentAdd) * ballSizeAdjustmentMult);
    }
}
