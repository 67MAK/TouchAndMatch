using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    int countTime = 3;
    [SerializeField] Text countdownText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while(countTime > 0)
        {
            countdownText.text = countTime.ToString();
            yield return new WaitForSeconds(1f);
            countTime -= 1;
        }
        countdownText.text = "Initiating...";
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
