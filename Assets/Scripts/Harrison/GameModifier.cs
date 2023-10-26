using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModifier : ModifierParent
{
    public enum PlayerToAffect
    {
        ChoosingPlayer,
        OpposingPlayer,
        BothPlayers
    }
    [Header("Game Stats")]
    public GameManager manager;
    public PlayerToAffect playerToAffect;
    [Header("Player Stats")]
    public GameObject playerModifier;
    [Header("Ball Stats")]
    public GameObject ballModifier;

    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        manager = GetComponentInParent<GameManager>();
        List<Paddle> paddlesToEffect = new List<Paddle>();
        switch (playerToAffect)
        {
            case PlayerToAffect.ChoosingPlayer:
                break;
            case PlayerToAffect.OpposingPlayer:
                break;
            case PlayerToAffect.BothPlayers:
                break;
        }
    }
}
