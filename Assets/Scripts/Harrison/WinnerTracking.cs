using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerTracking : MonoBehaviour
{
    public static WinnerTracking instance;
    public int winner;

    private void Awake()
    {
        Debug.Log(winner.ToString());
        if (instance != null)
        {
            Destroy(gameObject);
            Destroy(this);
        }
        else
        {
            instance = GetComponent<WinnerTracking>();
            DontDestroyOnLoad(gameObject);
        }
    }

    
}
