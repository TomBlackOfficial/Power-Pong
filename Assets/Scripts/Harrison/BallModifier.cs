using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallModifier : ModifierParent
{
    [Header("Ball Stats")]
    protected Ball myBall;
    [SerializeField] protected float ballSpeedAdd = 0;
    [SerializeField] protected float ballSpeedMult = 1;
    [SerializeField] protected float ballSizeAdjustmentAdd = 0;
    [SerializeField] protected float ballSizeAdjustmentMult = 1;
    [SerializeField] public bool needsPlayerAssignment = false;
    protected Paddle myPlayer;
    private void Awake()
    {
        if (activateable)
        {
            needsPlayerAssignment = true;
        }
    }
    public virtual void AssignPlayer(Paddle player)
    {
        if (myPlayer == null && player != null)
        {
            myPlayer = player;
        }
    }

    public override void InitializeValues()
    {
        base.InitializeValues();
        myBall = GetComponentInParent<Ball>();
    }

    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        float newSpeed = (myBall.speed + ballSpeedAdd) * ballSpeedMult;
        myBall.SetBallSpeed(newSpeed);
        float newSize = (myBall.size + ballSizeAdjustmentAdd) * ballSizeAdjustmentMult;
        myBall.SetBallSize(newSize);
    }

    public virtual void BallHitPlayer(Paddle player)
    {
        
    }
}
