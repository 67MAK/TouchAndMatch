using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private float level1Score, level2Score, level3Score;
    public float level1HighestScore, level2HighestScore, level3HighestScore;

    EasyFileSave myFile;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance == null)
        {
            Instance = this;
            StartProcess();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    public void SaveData()
    {
        if (level1Score >= level1HighestScore) level1HighestScore = level1Score;
        if (level2Score >= level2HighestScore) level2HighestScore = level2Score;
        if (level3Score >= level3HighestScore) level3HighestScore = level3Score;

        myFile.Add("level1HighScore", level1HighestScore);
        myFile.Add("level2HighScore", level2HighestScore);
        myFile.Add("level3HighScore", level3HighestScore);

        myFile.Save();
    }

    public void LoadData()
    {
        if (myFile.Load())
        {
            level1HighestScore = myFile.GetFloat("level1HighScore");
            level2HighestScore = myFile.GetFloat("level2HighScore");
            level3HighestScore = myFile.GetFloat("level3HighScore");
        }
    }

    public float Level1Score
    {
        get
        {
            return level1Score;
        }
        set
        {
            level1Score = value;
        }
    }
    public float Level2Score
    {
        get
        {
            return level2Score;
        }
        set
        {
            level2Score = value;
        }
    }
    public float Level3Score
    {
        get
        {
            return level3Score;
        }
        set
        {
            level3Score = value;
        }
    }

    void StartProcess()
    {
        myFile = new EasyFileSave();
        LoadData();
    }
}
