using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SnowAttack snowAttack;
    [SerializeField] private Health playerHealth;

    private void Update()
    {
        animator.SetBool("isRunning",playerMovement.IsRunning());
        animator.SetBool("isDigging",snowAttack.IsDiggingSnow());
        animator.SetBool("isThrowing",snowAttack.IsThrowingSnow());
        animator.SetBool("isDead",!playerHealth.isAlive());
    }
}
