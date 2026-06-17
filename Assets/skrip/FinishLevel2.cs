using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLevel2 : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI finishText;
    public GameObject finishPanel;

    [Header("Player")]
    public CarController carController;

    private bool isFinished = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (finishText != null)
            finishText.text = "";

        if (finishPanel != null)
            finishPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isFinished) return;

        if (other.CompareTag("Player"))
        {
            isFinished = true;

            if (finishText != null)
                finishText.text = "PERFECT GAME";

            if (finishPanel != null)
                finishPanel.SetActive(true);

            if (carController != null)
            {
                carController.DisableDrive();
                carController.enabled = false;
            }

            Debug.Log("PERFECT GAME");

            Time.timeScale = 0f;
        }
    }

    public void BackToLevelSelect()
    {
        Time.timeScale = 1f;
        MainMenu.openLevelPanelAfterLoad = true;
        SceneManager.LoadScene("Main Menu");
    }
}