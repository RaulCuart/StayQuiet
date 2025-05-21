using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public PlayerScript playerScript;

    public void openSettings()
    {
        if (!settingsMenu.activeInHierarchy)
        {
            settingsMenu.SetActive(true);
            Time.timeScale = 0f;
            playerScript.gameisPaused = true;
        }
    }
    public void closeSettings()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        playerScript.gameisPaused = false;
    }

    public void loadLastCheckpoint()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
        playerScript.gameisPaused = false;
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
