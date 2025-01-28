using UnityEngine;
using Oculus.Interaction;

public class ResetOnRelease : MonoBehaviour
{
    private Grabbable _grabbable;
    private ReturnToOriginalPosition _returnToOriginal;

    private void Awake()
    {
        _grabbable = GetComponent<Grabbable>();
        _returnToOriginal = GetComponent<ReturnToOriginalPosition>();

        if (_grabbable != null && _returnToOriginal != null)
        {
            _grabbable.WhenPointerEventRaised += HandlePointerEvent;
            Debug.Log($"{gameObject.name} - ResetOnRelease initialized.");
        }
        else
        {
            Debug.LogError($"{gameObject.name} - Missing Grabbable or ReturnToOriginalPosition component.");
        }
    }

    private void HandlePointerEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Unselect) // Trigger on release
        {
            Debug.Log($"{gameObject.name} - Released, resetting position.");
            _returnToOriginal.ResetToOriginalPosition();
        }
    }
}
