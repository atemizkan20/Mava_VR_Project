using UnityEngine;

public class CourtSceneSetup : MonoBehaviour
{
    void Start()
    {
        // Suppose each collectible object has a script "CollectibleItem" with an itemID
        // or you just rely on object names or tags.
        CollectibleItem[] items = FindObjectsOfType<CollectibleItem>();
        foreach (CollectibleItem item in items)
        {
            if (GameStateManager.Instance.IsItemCollected(item.itemID))
            {
                // This item was already collected, remove or disable it
                Destroy(item.gameObject); 
                // or item.gameObject.SetActive(false);
            }
        }

        // Reassign or fix anchor references for your persistent inventory (if needed)
        var inventoryVR = FindObjectOfType<InventoryVR>();
        if (inventoryVR != null)
        {
            // e.g., anchor for Court scene
            GameObject anchor = GameObject.Find("Inventory Anchor");
            if (anchor != null)
            {
                inventoryVR.Anchor = anchor;
            }
            
            // normal scale
            inventoryVR.Inventory.transform.localScale = new Vector3(10,10,10);
        }
    }
}
