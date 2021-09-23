using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTextScript : MonoBehaviour
{
    [SerializeField] private GameObject GamerManager;

    private string timeStr = "";
    private Text timeText;

    private void Start()
    {
        timeText = GetComponent<Text>();
    }
    void Update()
    {
        timeStr = GamerManager.GetComponent<TimeManager>().GetTimeString();
        timeText.text = timeStr;
    }
}
