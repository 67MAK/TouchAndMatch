using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen, levelsScreen;

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

    public void ExitButton()
    {
        Application.Quit();
    }
}
