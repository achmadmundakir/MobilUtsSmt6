using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Checkpoint UI")]
    public TextMeshProUGUI checkpointText;

    [Header("Countdown UI")]
    public TextMeshProUGUI countdownText;

    [Header("Timer UI")]
    public TextMeshProUGUI timerText;

    [Header("Message UI")]
    public TextMeshProUGUI messageText;

    [Header("Checkpoint Settings")]
    public int totalCheckpoints = 3;

    [Header("Timer Settings")]
    public bool useTimer = true;
    public float timeLimit = 60f;

    [Header("Player")]
    public CarController carController;

    private int currentCheckpoint = 0;
    private bool gameStarted = false;
    private bool gameEnded = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateCheckpointUI();
        UpdateTimerUI();

        if (messageText != null)
        {
            messageText.text = "";
        }

        if (carController != null)
        {
            carController.DisableDrive();
        }

        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        if (!gameStarted || gameEnded) return;

        if (useTimer)
        {
            timeLimit -= Time.deltaTime;

            if (timeLimit <= 0)
            {
                timeLimit = 0;
                UpdateTimerUI();
                GameOver("GAME OVER!\nWAKTU HABIS");
                return;
            }

            UpdateTimerUI();
        }
    }

    IEnumerator StartCountdown()
    {
        if (countdownText != null) countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        if (countdownText != null) countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        if (countdownText != null) countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        if (countdownText != null) countdownText.text = "GO!";

        gameStarted = true;

        if (carController != null)
        {
            carController.EnableDrive();
        }

        yield return new WaitForSeconds(1f);

        if (countdownText != null) countdownText.text = "";
    }

    public void AddCheckpoint()
    {
        if (gameEnded) return;

        currentCheckpoint++;

        if (currentCheckpoint > totalCheckpoints)
        {
            currentCheckpoint = totalCheckpoints;
        }

        UpdateCheckpointUI();

        Debug.Log("Checkpoint: " + currentCheckpoint + "/" + totalCheckpoints);
    }

    public bool IsAllCheckpointDone()
    {
        return currentCheckpoint >= totalCheckpoints;
    }

    public void LevelComplete()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (carController != null)
        {
            carController.DisableDrive();
            carController.enabled = false;
        }

        if (messageText != null)
        {
            messageText.text = "LEVEL 2 SELESAI!";
        }

        Debug.Log("LEVEL 2 SELESAI!");
    }

    public void GameOver(string message)
    {
        if (gameEnded) return;

        gameEnded = true;

        if (carController != null)
        {
            carController.DisableDrive();
            carController.enabled = false;
        }

        if (messageText != null)
        {
            messageText.text = message;
        }

        Debug.Log(message);
    }

    void UpdateCheckpointUI()
    {
        if (checkpointText != null)
        {
            checkpointText.text = "Checkpoint: " + currentCheckpoint + "/" + totalCheckpoints;
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeLimit);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}