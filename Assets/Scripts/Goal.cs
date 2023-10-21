using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayer1Goal)
            {
                Debug.Log("Player 2 Scored!");
                GameManager._instance.PlayerScored(2);
            }
            else
            {
                Debug.Log("Player 1 Scored!");
                GameManager._instance.PlayerScored(1);
            }
        }
    }
}
