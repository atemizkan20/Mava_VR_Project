using UnityEngine;

public class KeySlot : MonoBehaviour
{
    [Tooltip("Has the user placed a key item in this slot?")]
    public bool hasKey = false;

    private void OnTriggerEnter(Collider other)
    {
        // If the object that enters has the "OfficeKey" tag, we store that the key is in the slot
        if (other.CompareTag("OfficeKey"))
        {
            hasKey = true;
            Debug.Log("Key has been placed in the KeySlot.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the key leaves the slot, we revert hasKey
        if (other.CompareTag("OfficeKey"))
        {
            hasKey = false;
            Debug.Log("Key has been removed from the KeySlot.");
        }
    }
}
