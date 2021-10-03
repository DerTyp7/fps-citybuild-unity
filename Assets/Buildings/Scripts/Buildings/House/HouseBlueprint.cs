using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBlueprint : BuildingBlueprint
{
    
    


    private Transform houseCube;

    public override void Init()
    {
        //Haus cube ím Obj -> hier wird es benutzt zum material ändern
        houseCube = gameObject.transform.Find("HouseCube");
    }

    public override void WhileColliding()
    {        
        //Wenn es collidet soll der HouseCube IM Object verändert werden!
        //Das ist bei jedem Building anders
        houseCube.GetComponent<MeshRenderer>().material = collisionMat;
    }

    public override void WhileNotColliding()
    {
        //Das selbe wie bei "WhileColliding"
        houseCube.GetComponent<MeshRenderer>().material = blueprintMat;
    }
}
