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

    public void StartButton()
    {
        levelsScreen.SetActive(true);
    }
    public void CloseLevels()
    {
        levelsScreen.SetActive(false);
    }

    public void OptionsButton()
    {
        optionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void Level1Button()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsButton()
    {
        if(creditsIndex == 2) creditsIndex = 0;
        if (creditsIndex == 0) creditsText.text = "Muhammed Ali Kurtulbaþ";
        else if (creditsIndex == 1) creditsText.text = "170106109003";
        creditsIndex++;
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
