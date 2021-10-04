using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public string title = "New Building";
    public string description = "A cool new building";
    public Material blueprintMat;
    public Material collisionMat;


    public abstract void OnStartUp();
    public enum BuildingType
    {
        Housing,
        Storage,
        Decoration
    }
    
    public BuildingType buildingType;

    private void Start()
    {
        gameObject.AddComponent<BuildingBlueprint>();

        gameObject.GetComponent<BuildingBlueprint>().blueprintMat = blueprintMat;
        gameObject.GetComponent<BuildingBlueprint>().collisionMat = collisionMat;

        FindChildByTag("Building").SetActive(false);
        FindChildByTag("Blueprint").SetActive(true);

        OnStartUp();
    }

    public void EndBlueprint(bool place = false)
    {
        if (place)
        {
            FindChildByTag("Blueprint").SetActive(false);
            FindChildByTag("Building").SetActive(true);
            Destroy(gameObject.GetComponent<BuildingBlueprint>());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject FindChildByTag(string tag)
    {
        foreach(Transform child in gameObject.transform)
        {
            if(child.tag == tag)
            {
                return child.gameObject;
            }
        }

        return null;
    }
}
