using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] private GameObject terrain;
    
    public GameObject prefab;

    public TMPro.TextMeshProUGUI buildingText;

    int i = 0;
    Ray ray;

    private void Start()
    {
        buildingText = GameObject.Find("SelectionText").GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            List<GameObject> registry = GameObject.Find("GameManager").GetComponent<BuildingsRegistry>().GetRegistry();

            i++;
            if(i >= registry.Count)
            {
                i = 0;
            }

            prefab = registry[i];
            buildingText.text = prefab.name;
        }

        // Build Button Handler
        if (Input.GetButtonDown("Build"))
        { // Wenn man den Button 'B'
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (terrain.GetComponent<Collider>().Raycast(ray, out hitData, Mathf.Infinity))
            {
                Instantiate(prefab, hitData.point, Quaternion.identity);

            }
        }
    }
}
