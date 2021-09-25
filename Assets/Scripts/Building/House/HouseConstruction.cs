using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseConstruction : BuildingConstruction
{
    private GameObject gameManager;

    [Header("Needed Resources")]
    [SerializeField] private int neededWood = 10;

    [Header("Having Resources")]
    [SerializeField] private int havingWood = 0;

    public override void Init()
    {
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<EventLog>().CreateEvent("Construction: House");
    }

    public override bool CheckForResources()
    {
        if (havingWood == neededWood)
        {
            gameManager.GetComponent<EventLog>().CreateEvent("Construction: House: finished");
            return true;
        }
        return false;
    }
    
}
