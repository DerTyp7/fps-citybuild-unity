using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name = "New Item";
    public string uuid = "new_item";
    public Sprite icon = null;
    public int count = 1;

    public Item(int c = 1)
    {
        count = c;
    }
}
