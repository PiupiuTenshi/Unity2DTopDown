using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int ID;

    public void SetID(int id)
    {
        this.ID = id;
    }

    public int GetID()
    {
        return this.ID;
    }
}
