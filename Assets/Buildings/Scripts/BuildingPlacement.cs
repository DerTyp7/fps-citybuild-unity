using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject prefab;

    Ray ray;
    private Canvas hud;

    private void Start()
    {
        hud = GameObject.Find("HUD").GetComponent<Canvas>();
    }

    void Update()
    {
        // Build Button Handler
        if (Input.GetButtonDown("Build"))
        { // Wenn man den Button 'B'
             //Get HUD Canvas
            hud.enabled = false; //Hide HUD

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (terrain.GetComponent<Collider>().Raycast(ray, out hitData, Mathf.Infinity))
            {
                Instantiate(prefab, hitData.point, Quaternion.identity);

            }
        }
    }
}
