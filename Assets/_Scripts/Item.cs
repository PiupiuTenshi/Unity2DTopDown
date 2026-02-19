using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class Item : ScriptableObject
{
    [SerializeField] private int ID;

    [SerializeField] private GameObject itemPrefab;

    public void SetID(int id) => this.ID = id;
    public int GetID() => this.ID;
    public GameObject GetItemPrefab() => this.itemPrefab;
}
