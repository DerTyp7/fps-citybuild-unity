using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public int HotbarSlotNumber;
    public Item item;
    public GameObject itemImage;
    public GameObject HotbarSlotNum;

    private void Start()
    {
        itemImage = gameObject.transform.Find("Image").gameObject;
        HotbarSlotNum = gameObject.transform.Find("HotbarSlotNum").gameObject;

        HotbarSlotNum.GetComponent<TMPro.TextMeshProUGUI>().text = HotbarSlotNumber.ToString();
        gameObject.name = "HotbarSlot " + HotbarSlotNumber.ToString();
    }

    public void ChangeItem(Item i)
    {
        item = i;
        itemImage.GetComponent<Image>().sprite = item.icon;
    }

}
