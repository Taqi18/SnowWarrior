using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform camTransform;

    void Start()
    {
        camTransform = Camera.main.transform; 
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camTransform.forward);
    }
}