using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTimeModifier : BallModifier
{
    [Header("Special Settings")]
    private float baseSpeed;
    [SerializeField] private float distanceMultiplier;
    private float offset = 5f;
    private float direction = 1;

    void Update()
    {
        float speedMod;
        float distanceFromMySide = Mathf.Abs(myBall.transform.position.x + (offset * direction));
        speedMod = distanceFromMySide * distanceMultiplier;
        float currentSpeed = baseSpeed * speedMod;
        myBall.SetBallSpeed(currentSpeed);
    }
    public override void InitializeValues()
    {
        base.InitializeValues();
        baseSpeed = myBall.speed;
        direction = 1;
        if (!myPlayer.isPlayer1)
        {
            direction = -1;
        }
    }
    public override void BallHitPlayer(Paddle player)
    {
        base.BallHitPlayer(player);
        baseSpeed += player.knockback;
    }
}
