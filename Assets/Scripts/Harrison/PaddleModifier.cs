using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Modifiers/Paddle Modifier")]
public class PaddleModifier : ModifierParent
{
    [Header("Player Stats")]
    public Paddle[] _player = new Paddle[2];
    public float _playerSpeedAdd = 0;
    public float _playerSpeedMult = 1;
    public int _playerHeightAdjustmentAdd = 0;
    public int _playerHeightAdjustmentMult = 1;

    protected override void Start()
    {
        base.Start();
    }
}
