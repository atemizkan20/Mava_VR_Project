using UnityEngine;

public class UserSynch : MonoBehaviour
{
    public Transform foxHead; // Tilkinin başı
    public Transform vrCamera; // VR kamera

    void LateUpdate()
    {
        // Kamerayı tilkinin başına hizala
        vrCamera.position = foxHead.position;

        // Tilkinin başına göre kamerayı döndür, ancak eğikliği sıfırla
        Vector3 correctedRotation = foxHead.rotation.eulerAngles;
        correctedRotation.z = 0; // Z eksenindeki eğimi sıfırla
        correctedRotation.x = 0; // X eksenindeki eğimi sıfırla
        vrCamera.rotation = Quaternion.Euler(correctedRotation);
    }
}
