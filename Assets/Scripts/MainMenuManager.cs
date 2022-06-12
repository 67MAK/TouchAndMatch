using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen, levelsScreen;
    [SerializeField] Text creditsText;
    int creditsIndex = 0;
    int rand;
    private void Start()
    {
        rand = Random.Range(0, 4);
        if (rand == 0) FindObjectOfType<AudioManager>().Play("ThirstyRose");
        else if (rand == 1) FindObjectOfType<AudioManager>().Play("MadWorld");
        else if (rand == 2) FindObjectOfType<AudioManager>().Play("Exhale");
        else if (rand == 3) FindObjectOfType<AudioManager>().Play("EternalFire");
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
        if (rand == 0) FindObjectOfType<AudioManager>().Stop("ThirstyRose");
        else if (rand == 1) FindObjectOfType<AudioManager>().Stop("MadWorld");
        else if (rand == 2) FindObjectOfType<AudioManager>().Stop("Exhale");
        else if (rand == 3) FindObjectOfType<AudioManager>().Stop("EternalFire");
        SceneManager.LoadScene(1);
    }
    public void Level2Button()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if (rand == 0) FindObjectOfType<AudioManager>().Stop("ThirstyRose");
        else if (rand == 1) FindObjectOfType<AudioManager>().Stop("MadWorld");
        else if (rand == 2) FindObjectOfType<AudioManager>().Stop("Exhale");
        else if (rand == 3) FindObjectOfType<AudioManager>().Stop("EternalFire");
        SceneManager.LoadScene(2);
    }
    public void Level3Button()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if (rand == 0) FindObjectOfType<AudioManager>().Stop("ThirstyRose");
        else if (rand == 1) FindObjectOfType<AudioManager>().Stop("MadWorld");
        else if (rand == 2) FindObjectOfType<AudioManager>().Stop("Exhale");
        else if (rand == 3) FindObjectOfType<AudioManager>().Stop("EternalFire");
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

    public void ExitButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }
}
