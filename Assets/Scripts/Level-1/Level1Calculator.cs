using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Calculator : MonoBehaviour
{
    public static Level1Calculator Instance;

    [SerializeField] GameObject firstStarObj, secondStarObj, thirdStarObj;
    [SerializeField] Text wrongSelectText, timeLeftText, scoreText;
    bool firstStar, secondStar, thirdStar;
    public float Score = 0;
    public int wrongSelectCount = 0, showColorHintCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateScore()
    {
        Score += Timer.Instance.GetDuration() * 10;
        if(wrongSelectCount == 0)
        {
            Score += 1000f;
        }
        else if (wrongSelectCount > 6)
        {
            if (Score > 60f) Score -= 60f;
            else if (Score <= 60f) Score = 0f;
        }
        else if (wrongSelectCount > 15)
        {
            if (Score > 120f) Score -= 120f;
            else if (Score <= 120f) Score = 0f;
        }

        if (showColorHintCount > 0)
        {
            Score += showColorHintCount * 150f;
        }

        if (firstStar) Score += 50f;
        if (secondStar) Score += 100f;
        if (thirdStar) Score += 100f;
    }
    void CalculateStars()
    {
        if (Level2Manager.Instance.gameEnded) firstStar = true;
        if (wrongSelectCount <= 12) secondStar = true;
        if (Timer.Instance.GetDuration() > 59f) thirdStar = true;
    }

    public void SetEndGameText()
    {
        CalculateStars();
        CalculateScore();
        wrongSelectText.text = "Wrong Selections : " + wrongSelectCount;
        if (Timer.Instance.durationSecond > 10)
        {
            timeLeftText.text = "Time Left : 0" + Timer.Instance.durationMinute + ":" + Timer.Instance.durationSecond;
        }
        else if (Timer.Instance.durationSecond < 10)
        {
            timeLeftText.text = "Time Left : 0" + Timer.Instance.durationMinute + ":0" + Timer.Instance.durationSecond;
        }
        scoreText.text = "Total Score : " + Score;
        StartCoroutine(SetActiveStars());
    }

    IEnumerator SetActiveStars()
    {
        yield return new WaitForSeconds(1f);
        if (firstStar)
        {
            firstStarObj.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Stars");
            yield return new WaitForSeconds(1f);
        }
        if (secondStar)
        {
            FindObjectOfType<AudioManager>().Play("Stars");
            secondStarObj.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Stars");
            yield return new WaitForSeconds(1f);
        }
        if (thirdStar)
        {
            FindObjectOfType<AudioManager>().Play("Stars");
            thirdStarObj.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Stars");
        }
    }
}
