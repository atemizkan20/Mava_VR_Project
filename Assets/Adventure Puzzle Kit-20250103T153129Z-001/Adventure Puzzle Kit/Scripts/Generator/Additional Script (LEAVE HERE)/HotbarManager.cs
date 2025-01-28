using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using AdventurePuzzleKit.ThemedKey;

public class HotbarManager : MonoBehaviour
{
    [Header("Hotbar Slots")]
    public List<Image> hotbarSlots; // Hotbar slots with default backgrounds

    [Header("Slot Background Sprite")]
    public Sprite slotBackgroundSprite; // The default slot background

    private List<Key> inventoryItems = new List<Key>();

    private void Start()
    {
        UpdateHotbar(); // Initialize the hotbar
    }

    public void UpdateHotbar()
    {
        // Get items from inventory
        inventoryItems = TKInventory.instance._keyList;

        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            if (i < inventoryItems.Count)
            {
                // Combine the background and the item sprite
                CombineSprites(hotbarSlots[i], inventoryItems[i]._KeySprite);
                hotbarSlots[i].enabled = true; // Ensure the slot is visible
            }
            else
            {
                // Reset the slot to the default background
                hotbarSlots[i].sprite = slotBackgroundSprite;
                hotbarSlots[i].enabled = true; // Background should always be visible
            }
        }
    }

    private void CombineSprites(Image slot, Sprite itemSprite)
    {
        // Assign the slot background and overlay the item sprite
        slot.sprite = itemSprite; // Replace this line if you want advanced blending (e.g., shaders)
    }
}
