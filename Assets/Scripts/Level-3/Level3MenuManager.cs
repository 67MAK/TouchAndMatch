using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3MenuManager : MonoBehaviour
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
        if (!Level3Manager.Instance.gameEnded)
        {
            Level3Manager.Instance.PauseGameProcess();
        }
    }
    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueButton()
    {
        Level3Manager.Instance.pauseScreen.SetActive(false);
        Level3Manager.Instance.gamePaused = false;
        Time.timeScale = 1f;
        Level3Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void ShowColorsButton()
    {
        if (Level3Manager.Instance.isColorHiding && !Level3Manager.Instance.gameEnded && !Level3Manager.Instance.gamePaused)
        {
            //FindObjectOfType<AudioManager>().Play("ClickSound");
            StartCoroutine(Level3Calculator.Instance.ShowColorProcess());
        }
    }
}
