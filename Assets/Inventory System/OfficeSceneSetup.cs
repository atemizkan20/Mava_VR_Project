using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeSceneSetup : MonoBehaviour
{
    void Start()
    {
        // Re-find the persistent inventory
        var inventoryVR = FindObjectOfType<InventoryVR>();
        if (inventoryVR != null)
        {
            // Find the anchor in the Office scene
            GameObject anchor = GameObject.Find("Inventory Anchor");
            if (anchor != null)
            {
                inventoryVR.Anchor = anchor;
            }

            // Scale it down to, e.g., 50% of normal
            inventoryVR.Inventory.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
