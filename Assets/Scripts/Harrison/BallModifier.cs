using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Modifiers/Ball Modifier")]
public class BallModifier : ModifierParent
{
    [Header("Ball Stats")]
    public List<Ball> _ball = new List<Ball>();
    public float _ballSpeedAdd = 0;
    public float _ballSpeedMult = 1;
    public int _ballSizeAdjustmentAdd = 0;
    public int _ballSizeAdjustmentMult = 1;
    protected override void Start()
    {
        base.Start();
        UpdateBallList();
    }
    public void UpdateBallList()
    {
        _ball.Clear();
        Ball[] ball = FindObjectsByType<Ball>(FindObjectsSortMode.None);
        for (int b = 0; b < ball.Length; b++)
        {
            _ball.Add(ball[b]);
        }
    }
}
