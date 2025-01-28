using UnityEngine;
using Oculus.Interaction;

public class PickUpItem : MonoBehaviour
{
    [Header("Buttons for Inventory Pickup")]
    public KeyCode[] inventoryKeys =
    {
        KeyCode.JoystickButton0,
        KeyCode.JoystickButton2
    };

    private bool _isBeingHeld;
    private Grabbable _grabbable; // We'll store a reference to the Grabbable

    void Awake()
    {
        Debug.Log("PickUpItem: Awake called.");
        // Attempt to get the Grabbable component from the same GameObject
        _grabbable = GetComponent<Grabbable>();

        if (_grabbable != null)
        {
            // Subscribe to the events we just added
            _grabbable.OnGrabBeginEvent += OnGrabBegin;
            _grabbable.OnGrabEndEvent += OnGrabEnd;
            Debug.Log("PickUpItem: Subscribed to OnGrabBeginEvent and OnGrabEndEvent");
        }
        else
        {
            Debug.LogWarning("PickUpItem could not find a Grabbable component on this object.");
        }
    }

    void OnDestroy()
    {
        // Always good practice to unsubscribe so we don’t leak events
        if (_grabbable != null)
        {
            _grabbable.OnGrabBeginEvent -= OnGrabBegin;
            _grabbable.OnGrabEndEvent -= OnGrabEnd;
        }
    }

    private void Update()
    {
        // Only check for input if item is currently being held
        if (_isBeingHeld)
        {
            foreach (KeyCode key in inventoryKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    Debug.Log("Button pressed: " + key);
                    AddToInventory();
                    break;
                }
                if (OVRInput.GetDown(OVRInput.Button.One)) 
                {
                    Debug.Log("A or X pressed!");
                    AddToInventory();
                }
            }
        }
    }

    private void AddToInventory()
    {
        // Increase the player's item count
        SimpleInventory.Instance.AddItem();
        // Hide or destroy this item for the prototype
        gameObject.SetActive(false);
    }

    // -----------------------------------------------------------
    //  EVENT HANDLERS FOR GRAB
    // -----------------------------------------------------------
    private void OnGrabBegin()
    {
        Debug.Log($"{gameObject.name} was just grabbed!");  
        _isBeingHeld = true;
        Debug.Log("OnGrabBegin triggered on " + gameObject.name);
    }

    private void OnGrabEnd()
    {
        _isBeingHeld = false;
        Debug.Log("OnGrabEnd triggered on " + gameObject.name);
    }
}
