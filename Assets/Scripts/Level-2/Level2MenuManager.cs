using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2MenuManager : MonoBehaviour
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
        if (!Level2Manager.Instance.gameEnded)
        {
            Level2Manager.Instance.PauseGameProcess();
        }
    }

    public void ContinueButton()
    {
        Level2Manager.Instance.pauseScreen.SetActive(false);
        Level2Manager.Instance.gamePaused = false;
        Time.timeScale = 1f;
        Level2Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void ShowColorsButton()
    {
        if (Level2Manager.Instance.isColorHiding && !Level2Manager.Instance.gameEnded && !Level2Manager.Instance.gamePaused)
        {
            //FindObjectOfType<AudioManager>().Play("ClickSound");
            StartCoroutine(Level2Calculator.Instance.ShowColorProcess());
        }
    }
}
