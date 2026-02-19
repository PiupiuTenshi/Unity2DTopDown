using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    [SerializeField] private List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;

    private void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            if (itemPrefabs != null)
            {
                itemPrefabs[i].SetID(i + 1);
            }
        }

        foreach (Item item in itemPrefabs)
        {
            itemDictionary[item.GetID()] = item.GetItemPrefab();
        }
    }

    public GameObject GetItemPrefabs(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject itemPrefab);
        if (itemPrefab == null)
        {
            Debug.LogWarning("Don't have the item with" + itemID + "!!!");
        }
        return itemPrefab;
    }
}
