using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedControlModifier : BallModifier
{
    [Header("Special Settings")]
    private float baseSpeed;
    [SerializeField] private float speedMultiplier;

    public override void ActivateAbility()
    {
        base.ActivateAbility();
        float currentSpeed = baseSpeed * speedMultiplier;
        myBall.SetBallSpeed(currentSpeed);
    }
    public override void DeactivateAbility()
    {
        base.DeactivateAbility();
        myBall.SetBallSpeed(baseSpeed);
    }
    public override void BallHitPlayer(Paddle player)
    {
        base.BallHitPlayer(player);
        baseSpeed += player.knockback;
    }
}
