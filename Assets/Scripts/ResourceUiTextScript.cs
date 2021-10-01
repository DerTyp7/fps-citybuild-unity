using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUiTextScript : MonoBehaviour
{
    private ResourceManager resourceManager;


    private string str = "";
    private TMPro.TextMeshProUGUI textResource;

    private void Start()
    {
        resourceManager = GameObject.Find("GameManager").GetComponent<ResourceManager>();
        textResource = GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Update()
    {
        List<Item> inventory = new List<Item>();
        inventory = resourceManager.GetAllResources();
        str = "";
        foreach(Item i in inventory)
        {
            if(i != null)
            {
                str += i.count.ToString() + " " + i.name + "\n";
            }
            
        }

        textResource.text = str;
    }
}
