using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallModifier : ModifierParent
{
    protected List<Ball> _ball = new List<Ball>();

    protected override void Start()
    {
        base.Start();
        UpdateBallList();
    }
    protected void UpdateBallList()
    {
        _ball.Clear();
        Ball[] ball = FindObjectsByType<Ball>(FindObjectsSortMode.None);
        for (int b = 0; b < ball.Length; b++)
        {
            _ball.Add(ball[b]);
        }
    }
}
