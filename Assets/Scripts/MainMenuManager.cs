using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen, levelsScreen, muteMusicButton, muteSoundsButton;
    [SerializeField] Text creditsText;
    string musicStringToPlay;
    int creditsIndex = 0;
    int i = 0, j = 0;
    int rand;
    private void Start()
    {
        rand = Random.Range(0, 4);
        if (rand == 0) musicStringToPlay = "ThirstyRose";
        else if (rand == 1) musicStringToPlay = "MadWorld";
        else if (rand == 2) musicStringToPlay = "Exhale";
        else if (rand == 3) musicStringToPlay = "EternalFire";
        FindObjectOfType<AudioManager>().Play(musicStringToPlay);
        FindObjectOfType<AudioManager>().SetVolume("Waterfall", 0.01f);
        FindObjectOfType<AudioManager>().Play("Waterfall");
    }
    public void StartButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        levelsScreen.SetActive(true);
    }
    public void CloseLevels()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        levelsScreen.SetActive(false);
    }

    public void OptionsButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        optionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        optionsScreen.SetActive(false);
    }

    public void Level1Button()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        FindObjectOfType<AudioManager>().Stop(musicStringToPlay);
        SceneManager.LoadScene(1);
    }
    public void Level2Button()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        FindObjectOfType<AudioManager>().Stop(musicStringToPlay);
        SceneManager.LoadScene(2);
    }
    public void Level3Button()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        FindObjectOfType<AudioManager>().Stop(musicStringToPlay);
        SceneManager.LoadScene(3);
    }

    public void CreditsButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if (creditsIndex == 2) creditsIndex = 0;
        if (creditsIndex == 0) creditsText.text = "Muhammed Ali Kurtulbaþ";
        else if (creditsIndex == 1) creditsText.text = "170106109003";
        creditsIndex++;
    }
    public void MuteMusicButton()
    {
        if(i == 0)
        {
            FindObjectOfType<AudioManager>().Stop(musicStringToPlay);
            muteMusicButton.GetComponent<Image>().color = Color.red;
            i = 1;
        }
        else if(i == 1)
        {
            rand = Random.Range(0, 4);
            if (rand == 0) musicStringToPlay = "ThirstyRose";
            else if (rand == 1) musicStringToPlay = "MadWorld";
            else if (rand == 2) musicStringToPlay = "Exhale";
            else if (rand == 3) musicStringToPlay = "EternalFire";
            FindObjectOfType<AudioManager>().Play(musicStringToPlay);
            muteMusicButton.GetComponent<Image>().color = Color.green;
            i = 0;
        }
    }
    public void MuteSoundsButton()
    {
        if(j == 0)
        {
            FindObjectOfType<AudioManager>().isMuted = true;
            FindObjectOfType<AudioManager>().Stop(musicStringToPlay);
            FindObjectOfType<AudioManager>().Stop("Waterfall");
            muteSoundsButton.GetComponent<Image>().color = Color.red;
            j = 1;
        }
        else if(j == 1)
        {
            FindObjectOfType<AudioManager>().isMuted = false;
            FindObjectOfType<AudioManager>().Play(musicStringToPlay);
            FindObjectOfType<AudioManager>().Play("Waterfall");
            muteSoundsButton.GetComponent<Image>().color = Color.green;
            j = 0;
        }
    }
    public void ExitButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }
}
