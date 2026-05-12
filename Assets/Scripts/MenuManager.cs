using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DoomClone");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadDeathScreen()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    public void LoadVictoryScreen()
    {
        SceneManager.LoadScene("VictoryScreen");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("DoomClone");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}