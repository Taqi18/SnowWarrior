using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerMovementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    
    private void OnEnable()
    {
        InputHub.OnStickHold += MoveOnLeftStickVector;
    }

    private void MoveOnLeftStickVector(object sender, InputHub.OnStickHoldEventArgs e)
    {
        var moveVector = new Vector3(e.LeftStickVector.x, 0, e.LeftStickVector.y);

        if (moveVector.magnitude > 0.1f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(moveVector * playerMovementSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnDisable()
    {
        InputHub.OnStickHold -= MoveOnLeftStickVector;
    }
}
