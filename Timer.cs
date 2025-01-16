using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField ]TextMeshProUGUI timertext;
    float starttime;
    bool istiming = false;
    void Start()
    {
        StartTimer();
    }
    void Update()
    {
        if (istiming)
        {
            UpdateTimer();
        }
    }
     public void StartTimer()
    {
        starttime = Time.time;
        istiming = true;
    }

    public void StopTimer()
    {
        istiming = false;
        DisplayFinalTime();
    }

    void UpdateTimer()
    {
        float timeElapsed = Time.time - starttime;
        timertext.text = FormatTime(timeElapsed);
    }
    void DisplayFinalTime()
    {
        float totalTime = Time.time - starttime;
        Debug.Log("Total Time: " + FormatTime(totalTime));
        timertext.text = "Final Time: " + FormatTime(totalTime);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        //int milliseconds = Mathf.FloorToInt((time * 1000F) % 1000F);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
