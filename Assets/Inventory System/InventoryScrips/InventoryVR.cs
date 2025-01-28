using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVR : MonoBehaviour
{
    
    public GameObject Inventory;       // Parent object that holds TitleCanvas, slots, items, etc.
    public Canvas InventoryCanvas;     // The Canvas component for UI
    public GameObject Anchor;          // Where the UI appears
    bool UIActive;

    // If you want a coordinate to send it "far away," define it here:
    private Vector3 hiddenPosition = new Vector3(9999f, 9999f, 9999f);

    public Slot[] slots = new Slot[4];

    private void Start()
    {
        // Keep Inventory active, so items remain valid. 
        // We'll hide the Canvas and teleport away instead.
        if (InventoryCanvas != null)
        {
            InventoryCanvas.enabled = false;
        }
        UIActive = false;

        // Initially, place the inventory at hiddenPosition so it’s out of sight
        if (Inventory != null)
        {
            Inventory.transform.position = hiddenPosition;
        }
    }

    private void Update()
    {
        // Press Button Four (Y or B) to toggle inventory
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            UIActive = !UIActive;

            // Show or hide Canvas visuals
            if (InventoryCanvas != null)
            {
                InventoryCanvas.enabled = UIActive;
            }

            // If turning inventory ON, move it to the anchor
            // If turning inventory OFF, move it far away
            if (Inventory != null)
            {
                if (UIActive)
                {
                    // Teleport the inventory to the anchor
                    Inventory.transform.position = Anchor.transform.position;
                    // Adjust rotation as you see fit
                    Inventory.transform.eulerAngles = new Vector3(
                        Anchor.transform.eulerAngles.x + 15,
                        Anchor.transform.eulerAngles.y,
                        0
                    );
                }
                else
                {
                    // Teleport to hidden position
                    Inventory.transform.position = hiddenPosition;
                }
            }
        }

        // If you want the inventory to follow the anchor *every frame* while it's active:
        if (UIActive && Inventory != null && Anchor != null)
        {
            Inventory.transform.position = Anchor.transform.position;
            Inventory.transform.eulerAngles = new Vector3(
                Anchor.transform.eulerAngles.x + 15,
                Anchor.transform.eulerAngles.y,
                0
            );
        }
    }
}
