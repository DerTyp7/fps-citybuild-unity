using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{    
    public (float y, bool isTerrain) GetTerrainHit(float x, float z)
    {
        float y = 0;
        bool isTerrain = false;
        Vector3 position = new Vector3(x, 50, z);
        

        RaycastHit hit;

        if(Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity))
        {
            if(hit.transform.tag == "Terrain")
            {
                Debug.Log("Terrain Hit");
                y = hit.point.y;
                Debug.Log(hit.point.y);
                isTerrain = true;
            }
            else
            {
                y = hit.point.y;
            }
            
        }
        else
        {
            Debug.Log("Terrain not Hit");
        }

            

        return (y, isTerrain);
    }
}
