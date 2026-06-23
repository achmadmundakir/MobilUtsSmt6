using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Panel")]
    public GameObject pausePanel;


    public void PauseGame()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }


    public void ResumeGame()
    {
        pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }


    public void QuitGame()
    {
        Time.timeScale = 1f;


        // kasih tanda agar Main Menu membuka LevelPanel
        MainMenu.openLevelPanelAfterLoad = true;


        // nama harus sama dengan scene kamu
        SceneManager.LoadScene("Main Menu");
    }
}