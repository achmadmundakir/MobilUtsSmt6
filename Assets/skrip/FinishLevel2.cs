using UnityEngine;
using TMPro;

public class FinishLevel2 : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI finishText;

    [Header("Player")]
    public CarController carController;

    private bool isFinished = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (finishText != null)
        {
            finishText.text = "";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isFinished) return;

        if (other.CompareTag("Player"))
        {
            isFinished = true;

            if (finishText != null)
            {
                finishText.text = "PERFECT GAME";
            }

            if (carController != null)
            {
                carController.DisableDrive();
                carController.enabled = false;
            }

            Debug.Log("PERFECT GAME");

            Time.timeScale = 0f;
        }
    }
}