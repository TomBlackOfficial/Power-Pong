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
    [SerializeField] protected PlayerToAffect playerToAffect;
    protected Paddle myPlayer;
    [SerializeField] protected float playerSpeedAdd = 0;
    [SerializeField] protected float playerSpeedMult = 1;
    [SerializeField] protected float playerHeightAdjustmentAdd = 0;
    [SerializeField] protected float playerHeightAdjustmentMult = 1;
    [SerializeField] protected float playerKnockbackAdd = 0;
    [SerializeField] protected float playerKnockbackMult = 1;
    public PlayerToAffect GetPlayerToAffect()
    {
        return playerToAffect;
    }
    public override void InitializeValues()
    {
        base.InitializeValues();
        myPlayer = GetComponentInParent<Paddle>();
    }
    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        List<Paddle> paddlesToEffect = new List<Paddle>();
        switch (playerToAffect)
        {
            case PlayerToAffect.BothPlayers:
                paddlesToEffect.Add(GameManager.instance.player1Paddle);
                paddlesToEffect.Add(GameManager.instance.player2Paddle);
                break;
            default:
                paddlesToEffect.Add(myPlayer);
                break;
        }
        for (int p = 0; p < paddlesToEffect.Count; p++)
        {
            float newSpeed = (paddlesToEffect[p].speed + playerSpeedAdd) * playerSpeedMult;
            paddlesToEffect[p].SetSpeed(newSpeed);
            float newHeight = (paddlesToEffect[p].height + playerHeightAdjustmentAdd) * playerHeightAdjustmentMult;
            paddlesToEffect[p].SetPaddleHeight(newHeight);
            float newKnockback = (paddlesToEffect[p].knockback + playerKnockbackAdd) * playerKnockbackMult;
            paddlesToEffect[p].SetKnockBack(newKnockback);
        }
    }
}
