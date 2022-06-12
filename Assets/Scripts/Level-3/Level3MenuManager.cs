using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3MenuManager : MonoBehaviour
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
        FindObjectOfType<AudioManager>().Stop("Waterfall");
        FindObjectOfType<AudioManager>().Stop("Floating");
        FindObjectOfType<AudioManager>().Play("Click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        if (!Level3Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("Click");
            Level3Manager.Instance.PauseGameProcess();
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
        Level3Manager.Instance.pauseScreen.SetActive(false);
        Level3Manager.Instance.gamePaused = false;
        Time.timeScale = 1f;
        Level3Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void ShowColorsButton()
    {
        if (Level3Manager.Instance.isColorHiding && !Level3Manager.Instance.gameEnded && !Level3Manager.Instance.gamePaused)
        {
            FindObjectOfType<AudioManager>().Play("Click");
            StartCoroutine(Level3Calculator.Instance.ShowColorProcess());
        }
    }
}
