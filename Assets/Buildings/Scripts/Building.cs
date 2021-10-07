using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public string title = "New Building";
    public string description = "A cool new building";
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private GameObject blueprintPrefab;


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
        GameObject blueprint = Instantiate<GameObject>(blueprintPrefab);
        blueprint.transform.parent = gameObject.transform;
        
        OnStartUp();
    }

    public void Place(Transform t)
    {
        GameObject building = Instantiate<GameObject>(buildingPrefab);
        building.transform.position = t.position;
        building.transform.rotation = t.rotation;
        building.transform.parent = gameObject.transform;
    }

}
