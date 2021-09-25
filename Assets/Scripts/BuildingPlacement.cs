using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    TerrainCollider terrainCollider;
    public GameObject terrain;
    Ray ray;
    public float rotation = 0;
    [SerializeField] public GameObject building;
    [SerializeField] bool isPlacing = false;
    private Building currentBuilding;

    void Start()
    {
        currentBuilding = building.GetComponent<Building>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Build")) { // Wenn man den Button 'B'
            if (isPlacing) { // Wenn man 'B' zum zweiten mal dr�ckt
                isPlacing = false;
                currentBuilding.isColliding = 0;
                currentBuilding.isPlacing(false);
                building.SetActive(false); // Blueprint wird deaktiviert
            }
            else { // Wenn man zum ersten mal 'B' dr�ckt
                building.SetActive(true); // Blueprint wird aktiviert
                isPlacing = true;
            }
        }
        if (building.transform != null && isPlacing) {
            currentBuilding.isPlacing(true);
            getRaycastMousePosition();
            rotateObject(); 
            if (Input.GetMouseButtonDown(0) && currentBuilding.isColliding == 0) { // Wenn es nicht Collidiert und man Links Klickt
                isPlacing = false;
                currentBuilding.isPlacing(false);
                Instantiate(building, building.transform.position, building.transform.rotation); // Placed das Gebäude
                building.SetActive(false); // Blueprint wird deaktiviert
            }
        }
    }

    void getRaycastMousePosition() // Position of Mouse in World-Space
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (terrain.GetComponent<Collider>().Raycast(ray, out hitData, Mathf.Infinity))
        {
            building.transform.position = hitData.point;
        }
    }
    void rotateObject()
    {
        if (Input.GetButtonDown("CounterRotate")) { // If Player presses button 'Left ALT' + 'R'
            building.transform.Rotate(0, -10, 0); // Rotates the building counter clockwise
        }
        else if (Input.GetButtonDown("Rotate")) { // If Player presses button 'R'
            building.transform.Rotate(0, 10, 0); // Rotates the building clockwise
        }
    }
    
}
