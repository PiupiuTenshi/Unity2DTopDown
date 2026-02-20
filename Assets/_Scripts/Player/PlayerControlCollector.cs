using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControlCollector : MonoBehaviour
{
    private const string ITEM = "Item";
    private InventoryController inventoryController;

    private void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();  
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(ITEM))
        {
            ItemDisplay itemDisplay = collision.GetComponent<ItemDisplay>();
            if (itemDisplay != null)
            {
                bool isAddItem = inventoryController.AddItem(collision.gameObject);
                if (isAddItem)
                {
                    itemDisplay.PickUp();
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
