using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Die Sekunden werden gezählt

public class TimeManager : MonoBehaviour
{
    [Header("TimeManager")]
    [SerializeField] private float timePeriod = 0.02f;

    public int secondsOfDay = 0;


    private void Start()
    {
        InvokeRepeating("TimeUpdate", 1f, timePeriod);
    }
    private void TimeUpdate()
    {
        secondsOfDay++;
    }

    public float GetTimeOfDayFloat()
    {
        float timeOfDay = ((float)secondsOfDay / (float)3600);
        return timeOfDay;
    }
    public string GetTimeString(bool seconds = false)
    {
        TimeSpan time = TimeSpan.FromSeconds(secondsOfDay);
        string str = "";

        if (seconds)
        {
            str = time.ToString(@"hh\:mm\:ss");
        }
        else
        {
            str = time.ToString(@"hh\:mm");
        }
        

        return str;
    }
}
