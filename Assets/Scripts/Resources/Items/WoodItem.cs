using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodItem : Item
{
    public WoodItem(int c = 1)
    {
        count = c;
        name = "Wood";
        uuid = "item_wood"; 
    }
}
