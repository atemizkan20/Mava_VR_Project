using UnityEngine;
using TMPro; // Make sure you have "using TMPro;"

public class SimpleInventory : MonoBehaviour
{
    public static SimpleInventory Instance;

    [Header("Inventory Settings")]
    public int maxItems = 4; // Maximum number of items

    [Header("UI References")]
    public TextMeshProUGUI itemCounterTMP; // TextMeshPro instead of Text

    private int currentItemCount = 0;

    private void Awake()
    {
        // Simple Singleton
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

    public void AddItem()
    {
        if (currentItemCount < maxItems)
        {
            currentItemCount++;
            UpdateCounterUI();

            // If we've now reached (or exceeded) the max, move story forward
            if (currentItemCount >= maxItems)
            {
                Debug.Log("Inventory is full! Trigger next story event here.");
                // e.g., load new scene, enable something, etc.
            }
        }
    }

    private void UpdateCounterUI()
    {
        if (itemCounterTMP != null)
        {
            itemCounterTMP.text = $"Items: {currentItemCount}/{maxItems}";
        }
    }
}
