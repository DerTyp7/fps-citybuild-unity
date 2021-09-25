using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwitchBtn: Interactable
{
    public GameObject GameManager;
    public bool dark;


    public override string GetDescription()
    {
        if (dark)
        {
            
            return "Press [E] to skip to <color=red>night</color>.";
        }
        else
        {
            
            return "Press [E] to skip to <color=green>day</color>.";

        }
    }


    public override void Interact()
    {
  

        if (dark)
        {
            GameManager.GetComponent<EventLog>().CreateEvent("Tag!");
            GameManager.GetComponent<TimeManager>().secondsOfDay = 0;
        }
        else
        {
            GameManager.GetComponent<EventLog>().CreateEvent("Nacht!");
            GameManager.GetComponent<TimeManager>().secondsOfDay = 30000;
        }
    }
}
