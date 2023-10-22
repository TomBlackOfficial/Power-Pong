using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallModifier : ModifierParent
{
    [Header("Ball Stats")]
    public List<Ball> ball = new List<Ball>();
    public float ballSpeedAdd = 0;
    public float ballSpeedMult = 1;
    public int ballSizeAdjustmentAdd = 0;
    public int ballSizeAdjustmentMult = 1;
    protected override void Start()
    {
        base.Start();
        UpdateBallList();
    }
    public void UpdateBallList()
    {
        ball.Clear();
        Ball[] ballArray = FindObjectsByType<Ball>(FindObjectsSortMode.None);
        for (int b = 0; b < ballArray.Length; b++)
        {
            ball.Add(ballArray[b]);
        }
    }
}
