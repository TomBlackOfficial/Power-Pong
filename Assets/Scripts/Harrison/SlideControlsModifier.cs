using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideControlsModifier : PlayerModifier
{
    [SerializeField] private float slideAmount = 0.1f;
    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        myPlayer.SlidingControls(slideAmount);
    }
}
