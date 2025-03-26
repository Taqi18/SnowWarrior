using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Assign the player's transform
    public float smoothSpeed = 5f;
    public Vector3 offset;  // Adjust to position the camera correctly

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
