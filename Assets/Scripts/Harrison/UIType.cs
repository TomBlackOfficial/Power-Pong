using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIType : MonoBehaviour
{
    [SerializeField] private Gamemode.GameType thisType;
    public Gamemode.GameType GetUIType()
    {
        return thisType;
    }
}
