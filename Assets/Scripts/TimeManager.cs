using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Eine Minute geht 0.23 Sekunden

public class TimeManager : MonoBehaviour
{
    [Header("TimeManager")]
    [SerializeField] private float timePeriod = 3f;
    [SerializeField] private Text timeUI;

    public int minutes = 0;
    public int hours = 0;
    public string current_time = "";

    public int day = 1;
    public int month = 1;
    public int year = -5000;
    public string yearStr = "";
    public string current_date = "";

    private void Start()
    {
        InvokeRepeating("timeUpdate", 1f, timePeriod);
    }


    void timeUpdate()
    {
        //Count
        minutes += 1;

        countTime();
        countDate();

        timeUI.text = current_time;

        /*
        Debug.Log(current_time);
        Debug.Log(current_date);
        */
    }

    private void countTime()
    {
        //Time
        if (minutes >= 60)
        {
            minutes = 0;
            hours += 1;
        }


        //Current Time String
        if (hours <= 9)
        {
            current_time = "0" + hours + ":";
        }
        else
        {
            current_time = hours + ":";
        }

        if (minutes <= 9)
        {
            current_time += "0" + minutes;
        }
        else
        {
            current_time += minutes;
        }
    }

    private void countDate()
    {
        //Date
        if (hours >= 24)
        {
            hours = 0;
            day += 1;
        }
        else if (day > 31)
        {
            day = 1;
            month += 1;
        }
        else if (month > 12)
        {
            month = 1;
            year += 1;
        }

        //Format Year
        if (year < 0)
        {
            yearStr = year * -1 + "B.C.";
        }
        else if (year > 0)
        {
            yearStr = year + "A.C.";
        }
        else
        {
            yearStr = "0";
        }

        //current Date
        if (day <= 9)
        {
            current_date = "0" + day + "/";
        }
        else
        {
            current_date = day + "/";
        }

        if (month <= 9)
        {
            current_date += "0" + month + "/";
        }
        else
        {
            current_date += month + "/";
        }

        current_date += yearStr;
    }
}
