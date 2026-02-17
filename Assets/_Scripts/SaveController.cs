using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    private InventoryController inventoryController;
    [SerializeField] private Button saveButton;
    private const string PLAYER_TAG = "Player";
    private const string SAVE_FILE = "SaveData.json";
    private string saveLocation;
    
    private void Awake()
    {
        saveButton.onClick.AddListener(() =>
        {
            SaveGame();
        });
    }
    private void Start()
    {
        saveLocation = Path.Combine(Application.dataPath, SAVE_FILE); 
        inventoryController = FindObjectOfType<InventoryController>();

        LoadGame();
    }

    private void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            mapBound = FindAnyObjectByType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name,
            listInventorySaveData = inventoryController.GetInventorySaveData(),
        };
        saveData.SetPlayerPosition(GameObject.FindGameObjectWithTag(PLAYER_TAG).transform.position);

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    private void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag(PLAYER_TAG).transform.position = saveData.GetPlayerPosition();
            FindAnyObjectByType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBound).GetComponent<PolygonCollider2D>();
            inventoryController.SetInventoryItems(saveData.listInventorySaveData);
        }
        else
        {
            SaveGame();
        }
    }
}
