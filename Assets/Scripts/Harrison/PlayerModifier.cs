using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifier : ModifierParent
{
    public enum PlayerToAffect
    {
        ChoosingPlayer,
        OposingPlayer,
        BothPlayers
    }
    [Header("Player Stats")]
    public PlayerToAffect playerToAffect;
    public Paddle player;
    public float playerSpeedAdd = 0;
    public float playerSpeedMult = 1;
    public int playerHeightAdjustmentAdd = 0;
    public float playerHeightAdjustmentMult = 1;

    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        player = GetComponentInParent<Paddle>();
        List<Paddle> paddlesToEffect = new List<Paddle>();
        switch (playerToAffect)
        {
            case PlayerToAffect.BothPlayers:
                paddlesToEffect.Add(GameManager.instance.player1Paddle);
                paddlesToEffect.Add(GameManager.instance.player2Paddle);
                break;
            default:
                paddlesToEffect.Add(player);
                break;
        }
        for (int p = 0; p < paddlesToEffect.Count; p++)
        {
            float newSpeed = (paddlesToEffect[p].speed + playerSpeedAdd) * playerSpeedMult;
            paddlesToEffect[p].SetSpeed(newSpeed);
            int newHeight = (int)((float)(paddlesToEffect[p].height + playerHeightAdjustmentAdd) * playerHeightAdjustmentMult);
            paddlesToEffect[p].SetPaddleHeight(newHeight);
        }
    }
}
