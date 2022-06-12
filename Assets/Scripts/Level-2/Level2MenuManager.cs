using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2MenuManager : MonoBehaviour
{
    public void RestartButton()
    {
        FindObjectOfType<AudioManager>().Stop("Waterfall");
        FindObjectOfType<AudioManager>().Stop("Floating");
        FindObjectOfType<AudioManager>().Play("Click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        FindObjectOfType<AudioManager>().Stop("Waterfall");
        FindObjectOfType<AudioManager>().Stop("Floating");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        if (!Level2Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("Click");
            Level2Manager.Instance.PauseGameProcess();
        }
    }
    public void NextLevelButton()
    {
        FindObjectOfType<AudioManager>().Stop("Waterfall");
        FindObjectOfType<AudioManager>().Stop("Floating");
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Level2Manager.Instance.pauseScreen.SetActive(false);
        Level2Manager.Instance.gamePaused = false;
        Time.timeScale = 1f;
        Level2Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void ShowColorsButton()
    {
        if (Level2Manager.Instance.isColorHiding && !Level2Manager.Instance.gameEnded && !Level2Manager.Instance.gamePaused)
        {
            FindObjectOfType<AudioManager>().Play("Click");
            StartCoroutine(Level2Calculator.Instance.ShowColorProcess());
        }
    }
}
