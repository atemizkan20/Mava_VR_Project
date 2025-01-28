using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("UI / Visuals")]
    public Image slotImage;
    private Color originalColor;

    [Header("Slot Settings")]
    [Tooltip("How much to multiply the item scale by when placed in the slot.")]
    public float shrinkFactor = 0.3f;  

    [Header("Runtime Info")]
    public GameObject ItemInSlot; 

    void Start()
    {
        if (slotImage == null)
        {
            slotImage = GetComponentInChildren<Image>();
        }
        if (slotImage != null)
        {
            originalColor = slotImage.color;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (ItemInSlot != null) return;
        GameObject obj = other.gameObject;
        if (!IsItem(obj)) return;

        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            InsertItem(obj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ItemInSlot == null || other.gameObject != ItemInSlot) return;

        ResetColor();

        ItemInSlot = null;
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            item.inSlot = false;
            item.currentSlot = null;
        }

        other.transform.SetParent(null, true);
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>() != null;
    }

    void InsertItem(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Parent the object to this slot
        obj.transform.SetParent(this.transform, true);
        obj.transform.localPosition = Vector3.zero;

        // For any custom item logic
        Item item = obj.GetComponent<Item>();
        if (item != null)
        {
            obj.transform.localEulerAngles = item.slotRotation;
            item.inSlot = true;
            item.currentSlot = this;
        }

        // Shrink the item, if you’re using the shrinkFactor approach
        obj.transform.localScale = obj.transform.localScale * shrinkFactor;

        // Tell the slot which item is now inside
        ItemInSlot = obj;
        // If this item is to be permanently removed from the world once collected:
        string itemID = obj.name; // Or a custom itemID from a script
        GameStateManager.Instance.MarkItemCollected(itemID);
        // Check the item’s tag. If it’s "clue", use green; otherwise, gray
        if (obj.CompareTag("Clue"))
        {
            slotImage.color = Color.green;
            ClueCounter cc = FindObjectOfType<ClueCounter>();
            if (cc != null)
            {
                cc.IncrementClueCount();
            }
        }
        else if (obj.CompareTag("OfficeKey"))
        {
            slotImage.color = Color.yellow;
        }
        else
        {
            slotImage.color = Color.gray;
        }

    }


    public void ResetColor()
    {
        if (slotImage != null)
        {
            slotImage.color = originalColor;
        }
    }
}
