using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool firstLaunch = true;
    [SerializeField] private float ballSlowmodeSpeed = 0.01f;
    [SerializeField] private GameObject goalExplosionPrefab;
    [SerializeField] private float explosionTime = 2.5f;

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
    public GameObject cardSelectionScreen;
    public Dictionary<Gamemode.GameType, GameObject> scorePanels = new Dictionary<Gamemode.GameType, GameObject>();
    private List<UIHealthHelperScript> uiHelpers = new List<UIHealthHelperScript>();

    private int player1Score;
    private int player2Score;
    
    public Dictionary<string, RandomModifierSelection.PlayerWhoSelected> pickedModifiers = new Dictionary<string, RandomModifierSelection.PlayerWhoSelected>();

    public Paddle loser { get; private set; }
    public Paddle winner { get; private set; }

    private void Awake()
    {
        instance = this;
        loser = player1Paddle;
        UIType[] uiList = FindObjectsByType<UIType>(FindObjectsSortMode.None);
        for (int i = 0; i < uiList.Length; i++)
        {
            if (!scorePanels.ContainsKey(uiList[i].GetUIType()))
            {
                scorePanels.Add(uiList[i].GetUIType(), uiList[i].gameObject);
            }
        }
        if (gamemode.gameType == Gamemode.GameType.Health)
        {
            player1Score = gamemode.startingHP;
            player2Score = gamemode.startingHP;
        }
        firstLaunch = true;
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
        foreach (Gamemode.GameType type in scorePanels.Keys)
        {
            scorePanels[type].SetActive(false);
            if (type == gamemode.gameType)
            {
                scorePanels[type].SetActive(true);
            }
        }
        switch (gamemode.gameType)
        {
            case Gamemode.GameType.Health:
                UIHealthHelperScript[] helpers = scorePanels[Gamemode.GameType.Health].GetComponentsInChildren<UIHealthHelperScript>();
                for (int h = 0; h < helpers.Length; h++)
                {
                    uiHelpers.Add(helpers[h]);
                }
                UpdateHPUI();
                break;
            default:
                break;
        }
    }

    public void PlayerScored(int playerID)
    {
        if (playerID != 1 && playerID != 2)
            return;
        switch (gamemode.gameType)
        {
            case Gamemode.GameType.Classic:
                if (playerID == 1)
                {
                    winner = player1Paddle;
                    loser = player2Paddle;

                    player1Score++;
                    player1Text.text = player1Score.ToString();
                }
                else if (playerID == 2)
                {
                    winner = player2Paddle;
                    loser = player1Paddle;

                    player2Score++;
                    player2Text.text = player2Score.ToString();
                }
                break;
            case Gamemode.GameType.Health:
                if (playerID == 1)
                {
                    winner = player1Paddle;
                    loser = player2Paddle;

                    player2Score--;
                }
                else if (playerID == 2)
                {
                    winner = player2Paddle;
                    loser = player1Paddle;

                    player1Score--;
                }
                UpdateHPUI();
                break;
        }
        // Checking which player won the round

        StartCoroutine(GoalAnimation());
    }

    private void CheckWinCondition()
    {
        switch (gamemode.gameType)
        {
            case Gamemode.GameType.Classic:
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
                break;
            case Gamemode.GameType.Health:
                if (player1Score <= 0)
                {
                    GameOver(2);
                    return;
                }
                else if (player2Score <= 0)
                {
                    GameOver(1);
                    return;
                }
                break;
        }

        SelectModifiers();
    }

    public void SelectModifiers()
    {
        cardSelectionScreen.SetActive(true);
        CardSelection.instance.GetModifiers();
    }

    public void ModifiersSelected()
    {
        StartCountdown();
    }

    public void ApplyModifier(GameObject modifier)
    {
        if (modifier.TryGetComponent(out ModifierParent parent))
        {
            if (pickedModifiers.ContainsKey(parent.name))
            {
                if (loser.isPlayer1)
                {
                    if (pickedModifiers[parent.name] == RandomModifierSelection.PlayerWhoSelected.P2)
                    {
                        pickedModifiers[parent.name] = RandomModifierSelection.PlayerWhoSelected.Both;
                    }
                    else if (pickedModifiers[parent.name] != RandomModifierSelection.PlayerWhoSelected.Both)
                    {
                        pickedModifiers[parent.name] = RandomModifierSelection.PlayerWhoSelected.P1;
                    }
                }
                else
                {
                    if (pickedModifiers[parent.name] == RandomModifierSelection.PlayerWhoSelected.P1)
                    {
                        pickedModifiers[parent.name] = RandomModifierSelection.PlayerWhoSelected.Both;
                    }
                    else if (pickedModifiers[parent.name] != RandomModifierSelection.PlayerWhoSelected.Both)
                    {
                        pickedModifiers[parent.name] = RandomModifierSelection.PlayerWhoSelected.P2;
                    }
                }
            }
            else
            {
                RandomModifierSelection.PlayerWhoSelected player = RandomModifierSelection.PlayerWhoSelected.P2;
                if (loser.isPlayer1)
                {
                    player = RandomModifierSelection.PlayerWhoSelected.P1;
                }
                pickedModifiers.Add(parent.name, player);
            }
        }

        if (modifier.TryGetComponent(out BallModifier ballModifierScript))
        {
            ballModifiers.Add(modifier);
        }
        else if (modifier.TryGetComponent(out PlayerModifier playerModifierScript))
        {
            switch (playerModifierScript.GetPlayerToAffect())
            {
                case PlayerModifier.PlayerToAffect.ChoosingPlayer:
                    loser.AddModifier(modifier);
                    break;
                case PlayerModifier.PlayerToAffect.OposingPlayer:
                    winner.AddModifier(modifier);
                    break;
                case PlayerModifier.PlayerToAffect.BothPlayers:
                    loser.AddModifier(modifier);
                    winner.AddModifier(modifier);
                    break;
            }
        }
        else if (modifier.TryGetComponent(out GameModifier gameModifierScript))
        {
            AddModifier(modifier);
        }

        cardSelectionScreen.SetActive(false);
        CardSelection.instance.gameObject.GetComponent<CustomEventSystem>().StartEventSystem();
        StartCountdown();
    }

    private void GameOver(int winnerID)
    {
        if (winnerID != 1 && winnerID != 2)
            return;

        currentGameState = GameStates.GameOver;
        Debug.Log("Player " + winnerID + " is the winner!");
        SceneManager.LoadScene(0);
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
        if (firstLaunch)
        {
            firstLaunch = false;
        }
        else
        {
            ball.firstLaunch = firstLaunch;
        }
        for (int b = 0; b < ballModifiers.Count; b++)
        {
            ball.AddModifier(ballModifiers[b], player1Paddle);
        }
    }

    public void AddSubtractLives(PlayerModifier.PlayerToAffect playerToAffect, int hpToAdd)
    {
        switch (playerToAffect)
        {
            case PlayerModifier.PlayerToAffect.ChoosingPlayer:
                if (loser.isPlayer1)
                {
                    player1Score += hpToAdd;
                }
                else
                {
                    player2Score += hpToAdd;
                }
                break;
            case PlayerModifier.PlayerToAffect.OposingPlayer:
                if (winner.isPlayer1)
                {
                    player1Score += hpToAdd;
                }
                else
                {
                    player2Score += hpToAdd;
                }
                break;
            case PlayerModifier.PlayerToAffect.BothPlayers:
                player1Score += hpToAdd;
                player2Score += hpToAdd;
                break;
        }
        UpdateHPUI();
    }

    public void SetLives(PlayerModifier.PlayerToAffect playerToAffect, int hpToSet)
    {
        switch (playerToAffect)
        {
            case PlayerModifier.PlayerToAffect.ChoosingPlayer:
                if (loser.isPlayer1)
                {
                    player1Score = hpToSet;
                }
                else
                {
                    player2Score = hpToSet;
                }
                break;
            case PlayerModifier.PlayerToAffect.OposingPlayer:
                if (winner.isPlayer1)
                {
                    player1Score = hpToSet;
                }
                else
                {
                    player2Score = hpToSet;
                }
                break;
            case PlayerModifier.PlayerToAffect.BothPlayers:
                player1Score = hpToSet;
                player2Score = hpToSet;
                break;
        }
        UpdateHPUI();
    }

    private void UpdateHPUI()
    {
        for (int ui = 0; ui < uiHelpers.Count; ui++)
        {
            if (uiHelpers[ui].GetPlayer())
            {
                uiHelpers[ui].UpdateHealth(player1Score);
            }
            else if (!uiHelpers[ui].GetPlayer())
            {
                uiHelpers[ui].UpdateHealth(player2Score);
            }
        }
    }

    public void AddModifier(GameObject modifier)
    {
        GameObject thisObject = Instantiate(modifier, this.transform);
        thisObject.transform.parent = this.gameObject.transform;
        ModifierParent pMod;
        GameModifier mod;
        if (thisObject.TryGetComponent<ModifierParent>(out pMod))
        {
            if (!thisObject.TryGetComponent<GameModifier>(out mod))
            {
                Debug.LogError("Trying to add a non-Game Modifier as a Game Modifier.");
                Destroy(thisObject);
                return;
            }
        }
        else
        {
            Debug.LogError("Trying to add a non-modifier as a modifier.");
            Destroy(thisObject);
            return;
        }
        mod.InitializeValues();
        mod.StartModifierEffect();
    }

    private IEnumerator GoalAnimation()
    {
        Vector3 goalPosition = ball.transform.position;
        Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
        ballRB.velocity = ballRB.velocity.normalized * ballSlowmodeSpeed;
        player1Paddle.SetParalized(true);
        player2Paddle.SetParalized(true);
        GameObject explosion = Instantiate(goalExplosionPrefab, goalPosition, Quaternion.identity);
        yield return new WaitForSeconds(explosionTime);
        CheckWinCondition();
    }
}
