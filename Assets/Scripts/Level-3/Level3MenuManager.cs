using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level3MenuManager : MonoBehaviour
{
    [SerializeField] GameObject muteMusicButton, muteSoundsButton;
    int i = 0, j = 0;
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

    public void MuteMusicButton()
    {
        if (i == 0)
        {
            FindObjectOfType<AudioManager>().Stop("Floating");
            muteMusicButton.GetComponent<Image>().color = Color.red;
            i = 1;
        }
        else if (i == 1)
        {
            FindObjectOfType<AudioManager>().Play("Floating");
            muteMusicButton.GetComponent<Image>().color = Color.green;
            i = 0;
        }
    }
    public void MuteSoundsButton()
    {
        if (j == 0)
        {
            FindObjectOfType<AudioManager>().isMuted = true;
            FindObjectOfType<AudioManager>().Stop("Waterfall");
            FindObjectOfType<AudioManager>().Stop("Floating");
            muteSoundsButton.GetComponent<Image>().color = Color.red;
            j = 1;
        }
        else if (j == 1)
        {
            FindObjectOfType<AudioManager>().isMuted = false;
            FindObjectOfType<AudioManager>().Play("Waterfall");
            FindObjectOfType<AudioManager>().Play("Floating");
            muteSoundsButton.GetComponent<Image>().color = Color.green;
            j = 0;
        }
    }
}
