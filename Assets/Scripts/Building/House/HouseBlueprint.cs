using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBlueprint : BuildingBlueprint
{
    public Material collisionMat;
    
    public Material blueprintMat;
    private Transform houseCube;

    public override void Init()
    {
        houseCube = gameObject.transform.Find("HouseCube");
    }

    public override void WhileColliding()
    {        
        houseCube.GetComponent<MeshRenderer>().material = collisionMat;
    }

    public override void WhileNotColliding()
    {
        houseCube.GetComponent<MeshRenderer>().material = blueprintMat;
    }
}
