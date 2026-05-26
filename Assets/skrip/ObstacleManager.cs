using UnityEngine;
using TMPro;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager instance;

    [Header("UI")]
    public TextMeshProUGUI obstacleText;
    public TextMeshProUGUI messageText;

    [Header("Player")]
    public CarController carController;

    [Header("Obstacle Settings")]
    public int maxHit = 3;

    private int currentHit = 0;
    private bool gameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        UpdateObstacleUI();

        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    public void AddHit()
    {
        if (gameOver) return;

        currentHit++;

        if (currentHit > maxHit)
        {
            currentHit = maxHit;
        }

        UpdateObstacleUI();

        Debug.Log("Hit rintangan: " + currentHit + "/" + maxHit);

        if (currentHit >= maxHit)
        {
            GameOver();
        }
    }

    void UpdateObstacleUI()
    {
        if (obstacleText != null)
        {
            obstacleText.text = "Hit: " + currentHit + "/" + maxHit;
        }
    }

    void GameOver()
    {
        gameOver = true;

        if (messageText != null)
        {
            messageText.text = "GAME OVER!\nMENABRAK 3 RINTANGAN";
        }

        if (carController != null)
        {
            carController.DisableDrive();
            carController.enabled = false;
        }

        Debug.Log("GAME OVER! Menabrak 3 rintangan");

        // Menghentikan waktu game, termasuk timer
        Time.timeScale = 0f;
    }
}