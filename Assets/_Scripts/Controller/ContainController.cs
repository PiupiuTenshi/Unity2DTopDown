using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContainController : MonoBehaviour
{
    [SerializeField] protected int maxSlot;
    [SerializeField] protected GameObject containPanel;
    [SerializeField] protected GameObject slotPrefab;
    protected ItemDictionary itemDictionary;


    protected virtual void Awake()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();

    }

    public List<InventorySaveData> GetContainSaveData()
    {
        List<InventorySaveData> listInventorySaveData = new List<InventorySaveData>();
        foreach (Transform slotTransform in containPanel.transform)
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
                    // 
                }
            }
        }
        return listInventorySaveData;
    }

    public void SetContainItems(List<InventorySaveData> listInventorySaveData)
    {
        foreach (Transform child in containPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxSlot; i++)
        {
            Instantiate(slotPrefab, containPanel.transform);
        }

        foreach (InventorySaveData data in listInventorySaveData)
        {
            if (data.slotIndex < maxSlot)
            {
                Slot slot = containPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
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
