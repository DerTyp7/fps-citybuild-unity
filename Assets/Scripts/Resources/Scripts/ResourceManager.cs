using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager: MonoBehaviour
{
    [SerializeField] private List<Item> itemList;

    public void Remove(Item item)
    {
        itemList.Remove(item);
    }
    
    public void Add(Item item)
    {
        itemList.Add(item);
    }

}
