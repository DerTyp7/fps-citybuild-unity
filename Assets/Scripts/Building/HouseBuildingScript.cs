using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBuildingScript : Building
{
    [SerializeField] private string title = "House";
    [SerializeField] private string description = "A place for people to live in.";

    public override string GetTitle()
    {
        return title;
    }
    public override string GetDescription()
    {
        return description;
    }
}
