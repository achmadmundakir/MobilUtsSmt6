using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI finishText;

    [Header("Player")]
    public CarController carController;

    [Header("Scene Settings")]
    public string nextSceneName = "Level2";
    public float delayBeforeNextScene = 2f;

    private bool isFinished = false;

    void Start()
    {
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
                finishText.text = "FINISH! LANJUT KE LEVEL 2...";
            }

            if (carController != null)
            {
                carController.DisableDrive();
                carController.enabled = false;
            }

            Debug.Log("Level 1 selesai, pindah ke Level 2");

            StartCoroutine(LoadNextSceneAfterDelay());
        }
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeNextScene);
        SceneManager.LoadScene(nextSceneName);
    }
}