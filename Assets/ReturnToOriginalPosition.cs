using System.Collections;
using UnityEngine;

public class ReturnToOriginalPosition : MonoBehaviour
{
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private Rigidbody _rigidbody;

    [Tooltip("Time it takes for the object to return to its original position.")]
    [SerializeField] private float _returnDuration = 1.0f;

    private bool _isReturning = false;

    private void Start()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();
        Debug.Log($"{gameObject.name} - Original position stored: {_originalPosition}");
    }

    public void ResetToOriginalPosition()
    {
        if (!_isReturning)
        {
            Debug.Log($"{gameObject.name} - Starting return to original position.");
            StartCoroutine(MoveToOriginalPosition());
        }
    }

    private IEnumerator MoveToOriginalPosition()
    {
        _isReturning = true;

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true; // Stop physics while resetting
        }

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < _returnDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _returnDuration;

            transform.position = Vector3.Lerp(startPosition, _originalPosition, t);
            transform.rotation = Quaternion.Lerp(startRotation, _originalRotation, t);

            yield return null;
        }

        transform.position = _originalPosition;
        transform.rotation = _originalRotation;

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false; // Restore physics
        }

        _isReturning = false;
        Debug.Log($"{gameObject.name} - Reset complete.");
    }
}
