using UnityEngine;

public class FoxPawController : MonoBehaviour
{
    public Transform leftControllerAnchor; // Left controller anchor
    public Transform rightControllerAnchor; // Right controller anchor
    public Transform leftPaw; // Fox's left front paw bone
    public Transform rightPaw; // Fox's right front paw bone
    public Transform foxRoot; // Root of the fox's body (e.g., pelvis or main body transform)
    
    [Range(0f, 1f)] public float followSpeed = 0.5f; // Speed at which paws follow the anchors
    [Range(0f, 1f)] public float bodyFollowSpeed = 0.1f; // Speed at which the body follows the paw anchors

    void LateUpdate()
    {
        // Update paw positions and rotations
        if (leftControllerAnchor != null && leftPaw != null)
        {
            leftPaw.position = Vector3.Lerp(leftPaw.position, leftControllerAnchor.position, followSpeed);
            leftPaw.rotation = Quaternion.Slerp(leftPaw.rotation, leftControllerAnchor.rotation, followSpeed);
        }

        if (rightControllerAnchor != null && rightPaw != null)
        {
            rightPaw.position = Vector3.Lerp(rightPaw.position, rightControllerAnchor.position, followSpeed);
            rightPaw.rotation = Quaternion.Slerp(rightPaw.rotation, rightControllerAnchor.rotation, followSpeed);
        }

        // Move the fox's root body towards the average position of the front paws
        if (foxRoot != null)
        {
            Vector3 averagePawPosition = (leftPaw.position + rightPaw.position) / 2;
            foxRoot.position = Vector3.Lerp(foxRoot.position, averagePawPosition, bodyFollowSpeed);
        }
    }
}
