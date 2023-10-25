using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameStates
    {
        Playing,
        Paused,
        GameOver
    }

    public GameStates currentGameState { get; private set; } = GameStates.Paused;

    [Header("Settings")]
    public Gamemode gamemode;

    [Header("Ball")]
    [SerializeField] private Ball ballPrefab;
    [HideInInspector] public Ball ball;
    [SerializeField] private List<GameObject> ballModifiers = new List<GameObject>();

    [Header("Player 1")]
    public Paddle player1Paddle;
    public Goal player1Goal;
    [SerializeField] private List<GameObject> p1Modifier = new List<GameObject>();

    [Header("Player 2")]
    public Paddle player2Paddle;
    public Goal player2Goal;
    [SerializeField] private List<GameObject> p2Modifier = new List<GameObject>();

    [Header("UI")]
    public TextMesh player1Text;
    public TextMesh player2Text;
    public Animator cooldownAnim;

    private int player1Score;
    private int player2Score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCountdown();
        for (int p1m = 0; p1m < p1Modifier.Count; p1m++)
        {
            player1Paddle.AddModifier(p1Modifier[p1m]);
        }
        for (int p2m = 0; p2m < p2Modifier.Count; p2m++)
        {
            player2Paddle.AddModifier(p2Modifier[p2m]);
        }
    }

    public void PlayerScored(int playerID)
    {
        if (playerID != 1 && playerID != 2)
            return;

        // Checking which player won the round
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

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        // If scoreToWin is set to 0 it means the game is infinite
        if (gamemode.scoreToWin > 0)
        {
            // Check to see if either player won the game
            if (player1Score >= gamemode.scoreToWin)
            {
                GameOver(1);
                return;
            }
            else if (player2Score >= gamemode.scoreToWin)
            {
                GameOver(2);
                return;
            }
        }

        // If no one won the game then continue
        StartCountdown();
    }

    private void GameOver(int winnerID)
    {
        if (winnerID != 1 && winnerID != 2)
            return;

        currentGameState = GameStates.GameOver;
        Debug.Log("Player " + winnerID + " is the winner!");
    }

    private void ResetPosition()
    {
        player1Paddle.Reset();
        player2Paddle.Reset();
    }

    private void StartCountdown()
    {
        currentGameState = GameStates.Paused;

        ResetPosition();
        cooldownAnim.Play("Countdown");
    }

    public void StartRound()
    {
        currentGameState = GameStates.Playing;

        SpawnBall();
        player1Paddle.StartRound();
        player2Paddle.StartRound();
    }

    private void SpawnBall()
    {
        ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity).GetComponent<Ball>();
        ball.SetBallSpeed(gamemode.startingBallSpeed);
        for (int b = 0; b < ballModifiers.Count; b++)
        {
            ball.AddModifier(ballModifiers[b], player1Paddle);
        }
    }
}
