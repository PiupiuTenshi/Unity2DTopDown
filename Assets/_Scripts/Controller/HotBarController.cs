using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotBarController : ContainController
{
    private Key[] hotBarKey;

    protected override void Awake()
    {
        base.Awake();

        hotBarKey = new Key[maxSlot];
        for (int i = 0; i < maxSlot; i++)
        {
            hotBarKey[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
        }
    }

    private void Update()
    {
        for (int i = 0; i < maxSlot; i++)
        {
            if (Keyboard.current[hotBarKey[i]].wasPressedThisFrame)
            {
                UseItemInSlot(i);
            }
        }

    }

    private void UseItemInSlot(int slotIndex)
    {
        Slot slot = containPanel.transform.GetChild(slotIndex).GetComponent<Slot>();
        if (slot.currentItem != null)
        {
            ItemDisplay itemDisplay = slot.currentItem.GetComponent<ItemDisplay>();
            itemDisplay.UseItem();
            Destroy(itemDisplay.gameObject);
        }
    }



}
