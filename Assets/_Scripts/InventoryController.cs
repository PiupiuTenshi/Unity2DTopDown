using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private int slotCount;
    // [SerializeField] private GameObject[] itemPrefabs;

    private void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
        // for (int i = 0; i < slotCount; i++)
        // {
        //     Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
        //     if (i < itemPrefabs.Length)
        //     {
        //         GameObject item = Instantiate(itemPrefabs[i], slot.transform);
        //         item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //         slot.currentItem = item;
        //     }
        // }
    }

    public List<InventorySaveData> GetInventorySaveData()
    {
        List<InventorySaveData> listInventorySaveData = new List<InventorySaveData>();
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                ItemDisplay itemDisplay = slot.currentItem.GetComponent<ItemDisplay>();
                if (itemDisplay != null && itemDisplay.itemData != null)
                {
                    listInventorySaveData.Add(new InventorySaveData 
                    { 
                        itemID = itemDisplay.itemData.GetID(), 
                        slotIndex = slotTransform.GetSiblingIndex() 
                    });
                }
                else
                {
                    
                }
            }
        }
        return listInventorySaveData;
    }

    public void SetInventoryItems(List<InventorySaveData> listInventorySaveData)
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        foreach (InventorySaveData data in listInventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefabs(data.itemID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
        }
    }
}
