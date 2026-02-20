using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public List<InventorySaveData> listInventorySaveData;
    public List<InventorySaveData> listHotBarSaveData;
    public float xPosition;
    public float yPosition;
    public float zPosition;
    public string mapBound;

    public Vector3 GetPlayerPosition()
    {
        return new Vector3(xPosition,yPosition,zPosition);
    }

    public void SetPlayerPosition(Vector3 playerPosition)
    {
        xPosition = playerPosition.x;
        yPosition = playerPosition.y;
        zPosition = playerPosition.z;
    }
}
