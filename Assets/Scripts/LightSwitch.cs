using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    public GameObject GameManager;
    public Light m_Light;
    public bool isOn;

    private void Start()
    {
        UpdateLight();
    }

    private void UpdateLight()
    {
        m_Light.enabled = isOn;
    }

    public override string GetDescription()
    {
        if (isOn)
        {
            return "Press [E] to turn <color=red>off</color> the light.";
        }
        return "Press [E] to turn <color=green>on</color> the light.";
    }

    public override void Interact()
    {
        isOn = !isOn;

        if (isOn) {
            GameManager.GetComponent<EventLog>().CreateEvent("Licht2");
        }
        else
        {
            GameManager.GetComponent<EventLog>().CreateEvent("Licht1");
        }
        
        //Debug.Log("Click Light");
        UpdateLight();
    }
}
