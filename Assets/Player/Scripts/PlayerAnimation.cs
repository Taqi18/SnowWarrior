using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Health playerHealth;

    private void OnEnable()
    {
        InputHub.OnStickHold += ActionOnStickMove;
        InputHub.OnStickHoldReleased += ActionOnStickReleased;
        SnowAttack.OnDiggingSnow += ActionOnSnowDigging;
        SnowAttack.OnBallThrow += ActionOnSnowBallThrow;
    }

    private void Update()
    {
        animator.SetBool("isDead",!playerHealth.isAlive());
        
        Debug.Log(playerHealth.isAlive());
    }


    private void ActionOnSnowBallThrow(object sender, SnowAttack.OnBallThrowEventArgs e)
    {
        animator.SetBool("isThrowing",e.isThrowingBall);
    }

    private void ActionOnSnowDigging(object sender, SnowAttack.OnDiggingSnowEventArgs e)
    {
        animator.SetBool("isDigging",e.isDiggingSnow);
    }

    private void OnDisable()
    {
        InputHub.OnStickHold -= ActionOnStickMove;
        InputHub.OnStickHoldReleased -= ActionOnStickReleased;
        SnowAttack.OnDiggingSnow -= ActionOnSnowDigging;
        SnowAttack.OnBallThrow -= ActionOnSnowBallThrow;;
    }
    private void ActionOnStickReleased(object sender, EventArgs e)
    {
            animator.SetBool("isRunning",false);
    }

    private void ActionOnStickMove(object sender, InputHub.OnStickHoldEventArgs e)
    {
        var moveVector = new Vector3(e.LeftStickVector.x, 0, e.LeftStickVector.y);

        if (moveVector.magnitude > 0.1f)
        {
            animator.SetBool("isRunning",true);
        }

    }



}
