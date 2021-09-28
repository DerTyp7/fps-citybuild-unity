using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Resources/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string uuid = "new_item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    
}
