using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseBlueprint : BuildingBlueprint
{
    private MeshRenderer[] childrenMeshRenderer;

    public override void Init()
    {
        //Haus cube ím Obj -> hier wird es benutzt zum material ändern
        childrenMeshRenderer = gameObject.GetComponentsInChildren<MeshRenderer>();
    }

    public override void WhileColliding()
    {
        //Wenn es collidet soll der HouseCube IM Object verändert werden!
        //Das ist bei jedem Building anders
        foreach(MeshRenderer r in childrenMeshRenderer)
        {
            r.material = collisionMat;
        }
        
    }

    public override void WhileNotColliding()
    {
        //Das selbe wie bei "WhileColliding"
        foreach (MeshRenderer r in childrenMeshRenderer)
        {
            r.material = blueprintMat;
        }
    }
}
