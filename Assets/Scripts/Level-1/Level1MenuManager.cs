using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1MenuManager : MonoBehaviour
{

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        if (!Level1Manager.Instance.gameEnded)
        {
            Level1Manager.Instance.PauseGameProcess();
        }
    }
    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueButton()
    {
        Level1Manager.Instance.pauseScreen.SetActive(false);
        Level1Manager.Instance.gamePaused = false;
        Time.timeScale = 1f;
        Level1Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
}
