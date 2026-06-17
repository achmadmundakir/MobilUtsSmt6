using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panel Menu")]
    public GameObject mainPanel;
    public GameObject levelPanel;

    public static bool openLevelPanelAfterLoad = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (openLevelPanelAfterLoad)
        {
            mainPanel.SetActive(false);
            levelPanel.SetActive(true);

            openLevelPanelAfterLoad = false;
        }
        else
        {
            mainPanel.SetActive(true);
            levelPanel.SetActive(false);
        }
    }

    public void OpenLevelMenu()
    {
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        levelPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void PlayLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}