using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private EnemyAiMovement enemyAiMovement;
    [SerializeField] private EnemyAiSnowAttack enemyAiSnowAttack;
    [SerializeField] private Health enemyHealth;
    void Update()
    {
        
        enemyAnimator.SetBool("isRunning",enemyAiMovement.IsRunning());
       enemyAnimator.SetBool("isDigging",enemyAiSnowAttack.IsCollectingSnow());
       enemyAnimator.SetBool("isThrowing",enemyAiSnowAttack.IsThrowing());
       enemyAnimator.SetBool("isDead",!enemyHealth.isAlive());

       if (!enemyHealth.isAlive())
       {
           enemyAiMovement.enabled = false;
           enemyAiSnowAttack.enabled = false;
       }
    }
}
