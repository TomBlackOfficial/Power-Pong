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
    [SerializeField] private GameManager manager;
    [SerializeField] private PlayerToAffect playerToAffect;
    [Header("Player Stats")]
    [SerializeField] private GameObject playerModifier;
    [Header("Ball Stats")]
    [SerializeField] private GameObject ballModifier;

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
