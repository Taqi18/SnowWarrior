using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowAttack : MonoBehaviour
{
    
    public static EventHandler <OnDiggingSnowEventArgs>OnDiggingSnow;

    public static EventHandler <OnBallThrowEventArgs>OnBallThrow;
    public class OnDiggingSnowEventArgs : EventArgs
    {
        public bool isDiggingSnow;
    }
    public class OnBallThrowEventArgs : EventArgs
    {
        public bool isThrowingBall;
    }
    
    
    [SerializeField] private  GameObject snowBallPrefab;
    [SerializeField] private GameObject playerHand;
    [SerializeField] private float snowCollectionDelay;
    [SerializeField] private float snowBallThrowSpeed;
    private bool _isDiggingSnow=false;
    private bool _isSnowBallInHand=false;
    private GameObject snowBall;
    [SerializeField] private float throwAnimationDelay;
   [SerializeField] private Health playerHealth;

    private void OnEnable()
    {
        InputHub.OnScreenContact += ThrowSnowBallOnTapIfPossible;
    }
    
    private void OnDisable()
    {
        InputHub.OnScreenContact -= ThrowSnowBallOnTapIfPossible;
    }

    private void ThrowSnowBallOnTapIfPossible(object sender, InputHub.OnScreenContactEventArgs e)
    {
        var targetPosition = e.TouchWorldPosition;

       
        
            
        if(snowBall==null)return;
        var velocity = CalculateLaunchVelocity(playerHand.transform.position, targetPosition, snowBallThrowSpeed);
        transform.forward = new Vector3(velocity.normalized.x, 0, velocity.normalized.z);
        OnBallThrow?.Invoke(this, new OnBallThrowEventArgs {isThrowingBall = true});
        StartCoroutine(ThrowAnimationDelay(targetPosition));
    }

    private IEnumerator ThrowAnimationDelay(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(throwAnimationDelay/2);
       var velocity = CalculateLaunchVelocity(playerHand.transform.position, targetPosition, snowBallThrowSpeed);
        snowBall.transform.SetParent(null);
        snowBall.GetComponent<Rigidbody>().isKinematic = false;
        snowBall.GetComponent<Rigidbody>().velocity = velocity;
        snowBall.GetComponent<SphereCollider>().isTrigger = false;
        snowBall = null;
        _isSnowBallInHand = false;
        yield return new WaitForSeconds(throwAnimationDelay/2);
        OnBallThrow?.Invoke(this, new OnBallThrowEventArgs {isThrowingBall = false});
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
        OnDiggingSnow?.Invoke(this, new OnDiggingSnowEventArgs {isDiggingSnow = _isDiggingSnow });
        StartCoroutine(SnowCollectionDelay());
    }

    private IEnumerator SnowCollectionDelay()
    {
        yield return new WaitForSeconds(snowCollectionDelay);
        _isDiggingSnow = false;
        OnDiggingSnow?.Invoke(this, new OnDiggingSnowEventArgs {isDiggingSnow = _isDiggingSnow });
        snowBall = Instantiate(snowBallPrefab);
        snowBall.GetComponent<SnowBall>().isPlayerBall = true;
        snowBall.transform.position = playerHand.transform.position;
        snowBall.transform.rotation = playerHand.transform.rotation;
        snowBall.transform.SetParent( playerHand.transform);
        snowBall.GetComponent<SphereCollider>().isTrigger = true;
     //   snowBall.SetActive(false);
        _isSnowBallInHand = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<SnowBall>(out SnowBall snowBall))
        {
            if (!snowBall.isPlayerBall)
            {
                playerHealth.Damage();
            }
        }
    }
}
