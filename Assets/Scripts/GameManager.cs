using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("Ball")]
    public Ball ball;

    [Header("Player 1")]
    public Paddle player1Paddle;
    public Goal player1Goal;

    [Header("Player 2")]
    public Paddle player2Paddle;
    public Goal player2Goal;

    [Header("Score UI")]
    public TextMesh player1Text;
    public TextMesh player2Text;

    private int player1Score;
    private int player2Score;

    private void Awake()
    {
        _instance = this;
    }

    public void PlayerScored(int playerID)
    {
        if (playerID != 1 && playerID != 2)
            return;

        if (playerID == 1)
        {
            player1Score++;
            player1Text.text = player1Score.ToString();
        }
        else if (playerID == 2)
        {
            player2Score++;
            player2Text.text = player2Score.ToString();
        }

        ResetPosition();
    }

    private void ResetPosition()
    {
        ball.Reset();
        player1Paddle.Reset();
        player2Paddle.Reset();
    }
}
