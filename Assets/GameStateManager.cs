using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    // Example: store items that have been permanently “collected” or removed from the scene
    // Could be item IDs, or gameObject names, etc.
    private HashSet<string> collectedItemIDs = new HashSet<string>();

    // Store any other booleans like: "Have we used Key #1?" or "Door #2 is open?" etc.
    // For example:
    public bool puzzleDoorOpened = false;

    void Awake()
    {
        // Make this a persistent singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Public method for marking an item as collected
    public void MarkItemCollected(string itemID)
    {
        collectedItemIDs.Add(itemID);
    }

    // Check if an item was collected
    public bool IsItemCollected(string itemID)
    {
        return collectedItemIDs.Contains(itemID);
    }
    
    // If you want to remove an item from the "collected" set
    public void UnmarkItemCollected(string itemID)
    {
        if (collectedItemIDs.Contains(itemID))
        {
            collectedItemIDs.Remove(itemID);
        }
    }
}
