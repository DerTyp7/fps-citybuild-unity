using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseConstruction : BuildingConstruction
{
    [Header("Needed Resources")]
    [SerializeField] private int neededWood = 10;

    [Header("Having Resources")]
    [SerializeField] private int havingWood = 0;

    public override void Init()
    {
    }

    public override bool CheckForResources()
    {
        if (havingWood == neededWood)
        {
            return true;
        }
        return false;
    }
}
