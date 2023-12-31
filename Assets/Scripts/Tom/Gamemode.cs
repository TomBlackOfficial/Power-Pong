using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGamemode", menuName = "ScriptableObjects/Gamemode", order = 1)]
public class Gamemode : ScriptableObject
{
    public string gameModeName = "NewGamemode";
    public int scoreToWin = 7;
    public float startingBallSpeed = 4;
    public float startingPaddleSpeed = 5;
    public int startingHP = 0;
    public enum GameType
    {
        Classic,
        Health
    }
    public GameType gameType = GameType.Classic;
}
