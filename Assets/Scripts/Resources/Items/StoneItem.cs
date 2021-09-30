using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneItem : Item
{
    public StoneItem(int c = 1)
    {
        count = c;
        name = "Stone";
        uuid = "item_stone";
    }
}
