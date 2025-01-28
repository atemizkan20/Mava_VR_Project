using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // A simple structure to keep track of items across scenes
    // For example, store item names or IDs
    public List<string> itemIDs = new List<string>();

    void Awake()
    {
        // Singleton approach
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject when changing scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Example: add item to inventory
    public void AddItem(string itemID)
    {
        if (!itemIDs.Contains(itemID))
        {
            itemIDs.Add(itemID);
        }
    }

    public bool HasItem(string itemID)
    {
        return itemIDs.Contains(itemID);
    }
}
