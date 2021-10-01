using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTextScript : MonoBehaviour
{
    private GameObject GameManager;

    private string timeStr = "";
    private Text timeText;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        timeText = GetComponent<Text>();
    }
    void Update()
    {
        timeStr = GameManager.GetComponent<TimeManager>().GetTimeString();
        timeText.text = timeStr;
    }
}
