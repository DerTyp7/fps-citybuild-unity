using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public string title = "New Building";
    public string description = "A cool new building";

    //Refer to a BLUEPRINT from here

    public enum BuildingType
    {
        Housing,
        Storage,
        Decoration
    }

    public BuildingType buildingType;
}
