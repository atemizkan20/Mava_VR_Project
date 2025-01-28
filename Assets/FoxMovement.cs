using UnityEngine;

public class FoxMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hızı
    public float rotationSpeed = 720f; // Dönme hızı (derece/saniye)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Tilki üzerinde Rigidbody bulunamadı!");
        }
    }

    void Update()
    {
        // Klavye girişlerini al
        float horizontal = Input.GetAxis("Horizontal"); // A-D veya Sol-Sağ
        float vertical = Input.GetAxis("Vertical"); // W-S veya Yukarı-Aşağı

        // Hareket yönünü hesapla
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Hareketi uygula
        if (moveDirection.magnitude > 0.1f)
        {
            // Yönlendirme
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed / 360f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Hareket
            Vector3 moveOffset = moveDirection * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + transform.TransformDirection(moveOffset));
        }
    }
}

