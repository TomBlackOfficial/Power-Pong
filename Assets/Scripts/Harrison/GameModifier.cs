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
    [System.Serializable]
    public struct PlayerEffect
    {
        public PlayerModifier.PlayerToAffect playerToAffect;
        public int livesToGain;
        public int livesToSet;
    }
    [Header("Game Stats")]
    [SerializeField] private GameManager manager;
    [Header("Player Stats")]
    [SerializeField] private List<PlayerEffect> player = new List<PlayerEffect>();

    public override void StartModifierEffect()
    {
        base.StartModifierEffect();
        manager = GetComponentInParent<GameManager>();
        List<PlayerModifier.PlayerToAffect> tempList = new List<PlayerModifier.PlayerToAffect>();
        for (int p = 0; p < player.Count; p++)
        {
            if (!tempList.Contains(player[p].playerToAffect))
            {
                tempList.Add(player[p].playerToAffect);
                if (player[p].livesToGain != null && player[p].livesToGain != 0)
                {
                    manager.AddSubtractLives(player[p].playerToAffect, player[p].livesToGain);
                }
                if (player[p].livesToSet != null && player[p].livesToSet > 0)
                {
                    manager.SetLives(player[p].playerToAffect, player[p].livesToSet);
                }
            }
        }
    }
}
