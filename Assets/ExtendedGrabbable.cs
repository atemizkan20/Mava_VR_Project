using UnityEngine;
using Oculus.Interaction;              // So we can inherit from Grabbable
using Oculus.Interaction.Input;        // Common for pointer events, if needed

public class ExtendedGrabbable : Grabbable
{
    // A helper method to forcibly unselect this item
    // if the Interaction SDK doesn't automatically unselect upon release.
    public void ForceUnselectAll()
    {
        // If there are N selecting points, we forcibly send
        // an "Unselect" event for each.
        for (int i = 0; i < SelectingPointsCount; i++)
        {
            // Use a dummy pointerId (0), since we don't actually have a pointer ID
            ProcessPointerEvent(
                new PointerEvent(
                    0,
                    PointerEventType.Unselect,
                    new Pose(),
                    null
                )
            );
        }
    }

    public override void ProcessPointerEvent(PointerEvent evt)
    {
        base.ProcessPointerEvent(evt);

        Item item = GetComponent<Item>();
        if (item == null) return;

        // If user grabs it (Select), un-parent if it was in a slot
        if (evt.Type == PointerEventType.Select)
        {
            if (item.inSlot)
            {
                // remove from slot references
                if (item.currentSlot != null)
                {
                    item.currentSlot.ItemInSlot = null;
                    item.currentSlot.ResetColor();
                    item.currentSlot = null;
                }
                item.inSlot = false;

                // physically un-parent
                transform.SetParent(null, true);
            }
        }
    }
    void InsertItem(GameObject obj)
    {
        // If the item has an ID or we use the name as ID
        string itemID = obj.name; // or a custom script property

        // Add to persistent list
        InventoryManager.Instance.AddItem(itemID);

        // Optionally destroy the actual item object in the scene, or keep it in a slot
        // ...
    }

}

