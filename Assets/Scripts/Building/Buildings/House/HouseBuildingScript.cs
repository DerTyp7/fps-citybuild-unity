using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBuildingScript : Building
{
    private void Start()
    {
        title = "House";
        description = "A place to live in";
        buildingType = BuildingType.Housing;
    }

}
