using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] Text timerText;
    [SerializeField] Image timerCircle;

    float durationMinute, durationSecond, totalDuration;
    float currentTime;

    IEnumerator CO;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {

        }
    }

    private void OnEnable()
    {
        Debug.Log("Enabled Timer");
        CO = UpdateTimer();
        SetDuration(2f, 30f);
        SetTimerText();
        StartCoroutine(CO);
    }

    public void StartTimer()
    {
        StartCoroutine(CO);
    }
    public void StopTimer()
    {
        StopCoroutine(CO);
    }
    public void SetDuration(float min, float sec) 
    { 
        durationMinute = min; 
        durationSecond = sec;
        totalDuration = (durationMinute * 60f) + durationSecond;
        currentTime = totalDuration;
        timerCircle.fillAmount = 1f;
    }


    void SetTimerText()
    {
        if(durationSecond < 10)
        {
            timerText.text = "0" + durationMinute.ToString() + ":" + "0" + durationSecond.ToString();
        }
        else
        {
            timerText.text = "0" + durationMinute.ToString() + ":" + durationSecond.ToString();
        }

    }
    void SetCircle()
    {
        float setValue;
        currentTime -= 1;
        setValue = currentTime / totalDuration;
        timerCircle.fillAmount = setValue;
    }

    IEnumerator UpdateTimer()
    {
        Debug.Log("Numerator içi :: " + durationMinute);
        while(durationMinute >= 0)
        {
            yield return new WaitForSeconds(1f);
            if (durationSecond == 0)
            {
                if (durationMinute == 0) break;
                durationMinute -= 1;
                durationSecond = 59f;
            }
            else if(durationSecond > 0)
            {
                durationSecond -= 1;
            }
            SetTimerText();
            SetCircle();
        }
        SetTimerText();
        SetCircle();
        yield return new WaitForSeconds(1f);
    }
}
