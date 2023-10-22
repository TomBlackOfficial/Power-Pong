using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleModifier : ModifierParent
{
    [Header("Player Reference")]
    public Paddle[] _player = new Paddle[2];

    protected override void Start()
    {
        base.Start();
    }
}
