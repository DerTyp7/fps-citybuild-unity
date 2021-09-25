using UnityEngine;

public class BuildingSign : Interactable
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
        Debug.Log(parentBuilding.GetTitle());
        
        Debug.Log("interact");
    }
}
