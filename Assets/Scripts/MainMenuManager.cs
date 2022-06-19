using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen, levelsScreen, highscoresScreen, muteMusicButton, muteSoundsButton;
    [SerializeField] Text creditsText, level1ScoreText, level2ScoreText, level3ScoreText;
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
        FindObjectOfType<AudioManager>().SetVolume("Waterfall", 0.15f);
        FindObjectOfType<AudioManager>().Play("Waterfall");
    }
    void HowManyZeros(float score)
    {

    }
    public void StartButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        levelsScreen.SetActive(true);
    }

    public void HighscoresButton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        
        DataManager.Instance.LoadData();
        highscoresScreen.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = DataManager.Instance.level1HighestScore.ToString("0000");
        highscoresScreen.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = DataManager.Instance.level2HighestScore.ToString("0000");
        highscoresScreen.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = DataManager.Instance.level3HighestScore.ToString("0000");
        highscoresScreen.SetActive(true);

    }
    public void CloseHighscores()
    {
        highscoresScreen.gameObject.SetActive(false);
    }
    public void ResetLevel1Button()
    {
        DataManager.Instance.level1HighestScore = 0f;
        DataManager.Instance.Level1Score = 0f;
        DataManager.Instance.SaveData();
        highscoresScreen.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = DataManager.Instance.level1HighestScore.ToString("0000");
    }
    public void ResetLevel2Button()
    {
        DataManager.Instance.level2HighestScore = 0f;
        DataManager.Instance.Level2Score = 0f;
        DataManager.Instance.SaveData();
        highscoresScreen.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = DataManager.Instance.level2HighestScore.ToString("0000");
    }
    public void ResetLevel3Button()
    {
        DataManager.Instance.level3HighestScore = 0f;
        DataManager.Instance.Level3Score = 0f;
        DataManager.Instance.SaveData();
        highscoresScreen.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = DataManager.Instance.level3HighestScore.ToString("0000");
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
