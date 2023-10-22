using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Modifiers/Game Modifier")]
public class GameModifier : ModifierParent
{
    [Header("Game Stats")]
    public GameManager _manager;
    [Header("Player Stats")]
    public Paddle[] _player = new Paddle[2];
    public float _playerSpeedAdd = 0;
    public float _playerSpeedMult = 1;
    public int _playerHeightAdjustmentAdd = 0;
    public int _playerHeightAdjustmentMult = 1;
    [Header("Ball Stats")]
    public List<Ball> _ball = new List<Ball>();
    public float _ballSpeedAdd = 0;
    public float _ballSpeedMult = 1;
    public int _ballSizeAdjustmentAdd = 0;
    public int _ballSizeAdjustmentMult = 1;


    protected override void Start()
    {
        base.Start();
    }
}
