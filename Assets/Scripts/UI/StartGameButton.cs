using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartGameButton : MonoBehaviour
{

    public GameObject pausePanel;
    public void LoadMainScene()
    {
        Time.timeScale = 1;
        Data.numberOfZombies = 11;
        SceneManager.LoadScene("MainScene");
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadCredit()
    {
        SceneManager.LoadScene("Credit");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnPauseClicked()
	{

        pausePanel.SetActive(true);
        Time.timeScale = 0;
	}

    public void OnResumeClicked()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
