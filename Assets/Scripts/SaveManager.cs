using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public GameObject continueButton;
    public GameObject confirmationMenu;
    public GameObject menuPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("posX") && !continueButton.activeInHierarchy)
        {
            continueButton.SetActive(true);
        }
    }

    public void newGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }

    public void openNewGameMenu()
    {
        if (!PlayerPrefs.HasKey("posX"))
        {
            SceneManager.LoadScene("Game");
        } else
        {
            confirmationMenu.SetActive(true);
            menuPanel.SetActive(false);
        }

    }

    public void closeNewGameMenu()
    {
        confirmationMenu.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void continueGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
