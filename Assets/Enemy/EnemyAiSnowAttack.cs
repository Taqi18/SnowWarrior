using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAiSnowAttack : MonoBehaviour
{
    
    

    
    [SerializeField] private  GameObject snowBallPrefab;
    [SerializeField] private GameObject enemyHand;
    [SerializeField] private float snowCollectionDelay;
    [SerializeField] private float snowBallThrowSpeed;
    private bool _isDiggingSnow=false;
    private bool _isSnowBallInHand=false;
    private GameObject _snowBall;
    [SerializeField] private float throwAnimationDelay;
    [SerializeField] private EnemyAiMovement enemyAiMovement;
    [SerializeField] private GameObject player;
    private bool _isThrowing;
    [SerializeField] private float attackDistance;
    [SerializeField] private Health enemyHealth;
    private void Update()
    {
        if (!enemyAiMovement.IsRunning() && !_isDiggingSnow)
        {
            CreateSnowBall();
        }

        if (_isSnowBallInHand&& !_isThrowing  && Vector3.Distance(transform.position,player.transform.position)<=attackDistance)
        {
            ThrowSnowBallOnPlayer();
        }
    }

    private void ThrowSnowBallOnPlayer()
    {
        var targetPosition = player.transform.position;
            
        if(_snowBall==null)return;
        _isThrowing = true;
        var velocity = CalculateLaunchVelocity(enemyHand.transform.position, targetPosition, snowBallThrowSpeed);
        transform.forward = new Vector3(velocity.normalized.x, 0, velocity.normalized.z);
        StartCoroutine(ThrowAnimationDelay(targetPosition));
    }

    private IEnumerator ThrowAnimationDelay(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(throwAnimationDelay/2);
       var velocity = CalculateLaunchVelocity(enemyHand.transform.position, targetPosition, snowBallThrowSpeed);
        _snowBall.transform.SetParent(null);
        _snowBall.GetComponent<Rigidbody>().isKinematic = false;
        _snowBall.GetComponent<Rigidbody>().velocity = velocity;
        _snowBall.GetComponent<SphereCollider>().isTrigger = false;
        _snowBall = null;
        _isThrowing = false;
        _isSnowBallInHand = false;
        yield return new WaitForSeconds(throwAnimationDelay/2);
    }

    private Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 end, float speed)
    {
        Vector3 displacement = end - start;
        Vector3 displacementXZ = new Vector3(displacement.x, 0, displacement.z); 

        float horizontalDistance = displacementXZ.magnitude; 
        float verticalDistance = displacement.y; 
        float gravity = Mathf.Abs(Physics.gravity.y);

       
        float time = horizontalDistance / speed;

        
        float velocityY = (verticalDistance + 0.5f * gravity * time * time) / time;
        Vector3 velocityXZ = displacementXZ.normalized * speed; 

        return new Vector3(velocityXZ.x, velocityY, velocityXZ.z);
    }

    public void CreateSnowBall()
    {
        if(_isSnowBallInHand)return;
        _isDiggingSnow = true;

        StartCoroutine(SnowCollectionDelay());
    }

    private IEnumerator SnowCollectionDelay()
    {
        yield return new WaitForSeconds(snowCollectionDelay);
        _isDiggingSnow = false;
        _snowBall = Instantiate(snowBallPrefab);
        _snowBall.GetComponent<SnowBall>().isPlayerBall = false;
        _snowBall.transform.position = enemyHand.transform.position;
        _snowBall.transform.rotation = enemyHand.transform.rotation;
        _snowBall.transform.SetParent( enemyHand.transform);
        _snowBall.GetComponent<SphereCollider>().isTrigger = true;
     //   snowBall.SetActive(false);
        _isSnowBallInHand = true;
    }

    public bool IsCollectingSnow()
    {
        return _isDiggingSnow;
    }

    public bool IsThrowing()
    {
        return _isThrowing;
    }

    public bool IsDigging()
    {
        return _isDiggingSnow;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<SnowBall>(out SnowBall snowBall))
        {
            if (snowBall.isPlayerBall)
            {
                enemyHealth.Damage();
            }
        }
    }
}
