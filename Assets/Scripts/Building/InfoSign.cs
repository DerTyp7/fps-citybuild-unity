using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSign : Interactable
{
    private Building parentBuilding;

    private void Start()
    {
        parentBuilding = this.transform.parent.gameObject.GetComponent<Building>();
    }


    public override string GetDescription()
    {
        return "Press [E] to get <color=blue>info</color>.";
    }

    public override void Interact()
    {
        Debug.Log("interact");
    }
}

