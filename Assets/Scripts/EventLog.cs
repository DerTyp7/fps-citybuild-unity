using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLog : MonoBehaviour
{
    [Header("Event Log")]
    [SerializeField] private GameObject eventObject;
    [SerializeField] private Transform parentEventObject;
    Vector3 position = new Vector3 (Screen.width - 150, 134f, 0f);
    [SerializeField] GameObject[] events;
    void Start()
    {
        events = GameObject.FindGameObjectsWithTag("Event");

    }

    // Update is called once per frame
    void Update()
    {
        events = GameObject.FindGameObjectsWithTag("Event");
        //Debug.Log(events.Length);
        if(events.Length < 4)
        {
            switch (events.Length)
            {
                case 0:
                    position.y = 134f;
                    break;
                case 1:
                    position.y = 174f;
                    break;
                case 2:
                    position.y = 214f;
                    break;
                case 3:
                    position.y = 254f;
                    break;
            }

            //Relocate

            for (int i = 0; i < events.Length; i++)
            {
                switch (events.Length)
                {
                    case 1:
                        events[0].transform.position = new Vector3(Screen.width - 150, 134f, 0f);
                        break;
                    case 2:
                        events[0].transform.position = new Vector3(Screen.width - 150, 134f, 0f);
                        events[1].transform.position = new Vector3(Screen.width - 150, 174f, 0f);
                        break;
                    case 3:
                        events[0].transform.position = new Vector3(Screen.width - 150, 134f, 0f);
                        events[1].transform.position = new Vector3(Screen.width - 150, 174f, 0f);
                        events[2].transform.position = new Vector3(Screen.width - 150, 214f, 0f);
                        break;
                    case 4:
                        events[0].transform.position = new Vector3(Screen.width - 150, 134f, 0f);
                        events[1].transform.position = new Vector3(Screen.width - 150, 174f, 0f);
                        events[2].transform.position = new Vector3(Screen.width - 150, 214f, 0f);
                        events[3].transform.position = new Vector3(Screen.width - 150, 254f, 0f);
                        break;
                }
            }

        
        }
        
    }

    public void CreateEvent(string msg)
    {
        if(events.Length < 4)
        {
            Instantiate(eventObject, position, Quaternion.identity, parentEventObject);
            eventObject.GetComponent<EventScript>().ChangeText(msg);
        }
        
    }

}
