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

    public int Count(Item item)
    {
        int count = 0;

        foreach(Item i in itemList)
        {
            if(i == item)
            {
                count += 1;
            }
        }

        return count;
    }
}
