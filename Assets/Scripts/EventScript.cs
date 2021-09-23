using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScript : MonoBehaviour
{
    //[SerializeField] private Text text;
    void Start()
    {
        
        Invoke("Delete", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText(string msg)
    {
        Text t = GetComponentInChildren<Text>();
        t.text = msg;
    }

    void Delete()
    {
        Destroy(this.gameObject);
    }
}
