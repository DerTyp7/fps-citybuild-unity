using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager: MonoBehaviour
{
    [SerializeField] private GameObject[] storageBuildings;

    private void Start()
    {
        storageBuildings = GameObject.FindGameObjectsWithTag("StorageBuilding");
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Item wood = new WoodItem(10);
            storageBuildings[0].GetComponent<StorageBuilding>().Add(wood);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Item stone = new StoneItem(12);
            storageBuildings[0].GetComponent<StorageBuilding>().Add(stone);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GetAllResources();
        }
    }*/


    public List<Item> GetAllResources()
    {
        List<Item> inventory = new List<Item>();

        //Für jedes StorageBuilding
        foreach(GameObject b in storageBuildings)
        {
            List<Item> buildingInv = b.GetComponent<StorageBuilding>().Getinventory();

            //Add items to already existing item +=
            foreach (Item item in buildingInv)
            { 
                foreach(Item i in inventory)
                {
                    if(i.uuid == item.uuid)
                    {
                        i.count += item.count;
                        buildingInv.Remove(item);
                    }
                }
            }

            //Add den Rest
            foreach(Item item in buildingInv)
            {
                inventory.Add(item);
            }
        }

        /*
        Debug.Log(inventory);
        Debug.Log(inventory[0].count);
        Debug.Log(inventory[1].count);*/

        return inventory;
    }
}
