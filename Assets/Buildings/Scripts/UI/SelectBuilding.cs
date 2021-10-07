using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuilding : MonoBehaviour
{
    public GameObject select;
    public void Select(GameObject select)
    {
        GameObject.Find("FirstPerson Player").GetComponent<BuildingPlacement>().prefab = select;
    }
}
