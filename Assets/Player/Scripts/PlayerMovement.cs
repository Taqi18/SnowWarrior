using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerMovementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private SnowAttack snowAttack;
    private bool _isRunning = false;
    private void OnEnable()
    {
        InputHub.OnStickHold += MoveOnLeftStickVector;
        InputHub.OnStickHoldReleased += StopMovementOnLeftStickReleased;
    }
    private void OnDisable()
    {
        InputHub.OnStickHold -= MoveOnLeftStickVector;
        InputHub.OnStickHoldReleased -= StopMovementOnLeftStickReleased;
    }

    private void StopMovementOnLeftStickReleased(object sender, EventArgs e)
    {
        _isRunning = false;
    }

    private void MoveOnLeftStickVector(object sender, InputHub.OnStickHoldEventArgs e)
    {

        if (snowAttack.IsDiggingSnow() || (snowAttack.IsThrowingSnow() && snowAttack.IsSnowBallInHand()))
        {
            _isRunning = false;
            return;
        }
        
        var moveVector = new Vector3(e.LeftStickVector.x, 0, e.LeftStickVector.y);

        if (moveVector.magnitude > 0.1f) 
        {
            _isRunning = true;
            Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(moveVector * playerMovementSpeed * Time.deltaTime, Space.World);
        }

    }

    public bool IsRunning()
    {
       return _isRunning;
    }
}
