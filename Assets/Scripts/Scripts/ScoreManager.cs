using System.Collections;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEditor.VersionControl;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private int maxScore = 5;

    private NetworkVariable<int> leftScore = new NetworkVariable<int>(0);
    private NetworkVariable<int> rightScore = new NetworkVariable<int>(0);
    private NetworkVariable<bool> gameOver = new NetworkVariable<bool>(false);

    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject startButton;

    private void Awake()
{
    Instance = this;

    if (leftScoreText == null)
        leftScoreText = GameObject.Find("leftScore").GetComponent<TextMeshProUGUI>();

    if (rightScoreText == null)
        rightScoreText = GameObject.Find("rightScore").GetComponent<TextMeshProUGUI>();

    if (winText == null)
        winText = GameObject.Find("winText").GetComponent<TextMeshProUGUI>();
}

    public override void OnNetworkSpawn()
    {
        Debug.Log(startButton);
        leftScore.OnValueChanged += OnLeftScoreChanged;
        rightScore.OnValueChanged += OnRightScoreChanged;
        gameOver.OnValueChanged += OnGameOverChanged;

        UpdateLeftScore(leftScore.Value);
        UpdateRightScore(rightScore.Value);

        winText.gameObject.SetActive(false);
        if (!IsServer){
        startButton.SetActive(false);
        }
    
    }

    public override void OnNetworkDespawn()
    {
        leftScore.OnValueChanged -= OnLeftScoreChanged;
        rightScore.OnValueChanged -= OnRightScoreChanged;
        gameOver.OnValueChanged -= OnGameOverChanged;
    }


    public void IncrementLeftScore()
    {
        if (!IsServer || gameOver.Value) return;

        leftScore.Value++;
        CheckForGameOver();
    }

    public void IncrementRightScore()
    {
        if (!IsServer || gameOver.Value) return;

        rightScore.Value++;
        CheckForGameOver();
    }

    private void CheckForGameOver()
{
    if (leftScore.Value >= maxScore)
    {
        gameOver.Value = true;
    }
    else if (rightScore.Value >= maxScore)
    {
        gameOver.Value = true;
    }
}


    private void OnLeftScoreChanged(int oldValue, int newValue)
    {
        UpdateLeftScore(newValue);
    }

    private void OnRightScoreChanged(int oldValue, int newValue)
    {
        UpdateRightScore(newValue);
    }

    private void OnGameOverChanged(bool oldValue, bool newValue)
{
    if (!newValue) return;

    if (leftScore.Value >= maxScore)
        ShowWinner("Player 1 Wins!");
    else if (rightScore.Value >= maxScore)
        ShowWinner("Player 2 Wins!");

    StopGameplay();
    ShowStartButtonClientRpc();
}
[ClientRpc]
private void ShowStartButtonClientRpc()
{
    startButton.SetActive(true);
}
[ClientRpc]
private void HideStartButtonClientRpc()
{
    startButton.SetActive(false);
}


    private void UpdateLeftScore(int value)
    {
        leftScoreText.text = value.ToString();
    }

    private void UpdateRightScore(int value)
    {
        rightScoreText.text = value.ToString();
    }

    private void ShowWinner(string message)
    {
        winText.text = message;
        winText.gameObject.SetActive(true);
    }


    private void StopGameplay()
{
    if (!IsServer) return;

    BallMovement ball = FindObjectOfType<BallMovement>();
    if (ball != null)
    {
        ball.StopBall();
    }
}
public bool isGameOver()
{
    return gameOver.Value;
}
public void StartGame()
{
    if (!IsServer) return;

    leftScore.Value = 0;
    rightScore.Value = 0;

    gameOver.Value = false;

    winText.gameObject.SetActive(false);

    BallMovement ball = FindObjectOfType<BallMovement>();
    if (ball != null)
    {
        ball.ResetBall();
    }

    startButton.SetActive(false); // local hide for server
    HideStartButtonClientRpc();   // hide for all clients
}
public void OnStartButtonClicked()
{
    // Only server does the game start
    if (!IsServer) return;

    StartGame();
}
}