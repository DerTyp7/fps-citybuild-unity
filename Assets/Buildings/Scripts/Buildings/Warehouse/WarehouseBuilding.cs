using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseBuilding : StorageBuilding
{

    public override void Init()
    {
        title = "Warehouse";
        description = "A place to store your resources";
        inventorySpace = 500;
    }

}
