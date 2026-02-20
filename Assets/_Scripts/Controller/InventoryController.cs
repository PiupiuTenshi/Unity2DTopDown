using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class InventoryController : ContainController
{

    public bool AddItem(GameObject itemPrefab)
    {
        foreach(Transform slotTransform in containPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;  
                return true;
            }
        }
        
        return false;
    }
}
