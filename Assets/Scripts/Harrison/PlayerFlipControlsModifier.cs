using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipControlsModifier : PlayerModifier
{
    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        myPlayer.FlipControls();
    }
}
