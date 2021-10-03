using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBuilding : Building
{
    [SerializeField] private List<Item> inventory = new List<Item>();
    public int inventorySpace;



    public void Awake()
    {
        buildingType = BuildingType.Storage;

    }
    public void Add(Item item)
    {
        if(GetFreeSpace() >= item.count)
        {
            bool added = false;
            //Check if the Item can get stacked
            foreach (Item i in inventory)
            {
                if (i.uuid == item.uuid)
                {
                    i.count += item.count;
                    added = true;
                    return;
                }
                added = false;


            }

            //If foreach does not work just ADD (List is empty)
            if (!added)
            {
                inventory.Add(item);
            }
           
        }
        else
        {
            Debug.Log("Inventory Full");
        }
        
        //TODO mach wenn nicht ganz voll, dass dann so viele items added werden wie platz ist
        //Sonst wird bei 20 Holz KOMPLETT nein gesagt weil/obowhl 19 Space noch da ist
    }

    public void Remove(Item item)
    {
        //Check if the Item can get stacked
        foreach (Item i in inventory)
        {
            if (i.uuid == item.uuid)
            {
                if(i.count > item.count)
                {
                    i.count -= item.count;
                }else if(i.count <= item.count)
                {
                    //!!!Muss eventuell später anders gehandelt werden!!!
                    inventory.Remove(i); //Wenn du mehr entfernst als im Inventar ist, dann wird das Item einfach komplett removed
                }
            }
        } 
    }

    public int GetCountOfItem(Item item)
    {
        int count = 0;
        foreach(Item i in inventory)
        {
            if(i.uuid == item.uuid)
            {
                count += i.count;
            }
        }
        return count;

    }

    public int GetUsedSpace()
    {
        int usedSpace = 0;

        foreach(Item item in inventory)
        {
            usedSpace += item.count;
        }

        return usedSpace;
    }

    public int GetFreeSpace()
    {
        return inventorySpace - GetUsedSpace();
    }
    public List<Item> Getinventory()
    {
        return inventory;
    }
}
