using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingConstruction : MonoBehaviour
{

    public GameObject building;

    public abstract bool CheckForResources();
    public abstract void Init();

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (CheckForResources())
        {
            Instantiate(building, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
