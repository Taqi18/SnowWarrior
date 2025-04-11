using System;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public bool _isPlayerBall;
    private Rigidbody _snowBallRb;
    private SphereCollider _snowBallCollider;

    private void OnEnable()
    {
        _snowBallCollider = GetComponent<SphereCollider>();
        _snowBallRb = GetComponent<Rigidbody>();
        
    }
    
    public void ChangeSnowBallInToThrowState(Vector3 velocity)
    {
        transform.SetParent(null);
        _snowBallRb.isKinematic = false;
        _snowBallRb.velocity = velocity;
        _snowBallCollider.isTrigger = false;
    }

    public void SetSnowBallInHand(Transform handTransform,bool isPlayerBall)
    {
        this._isPlayerBall = isPlayerBall;
        transform.position = handTransform.position;
        transform.rotation = handTransform.rotation;
        transform.SetParent(handTransform);

        _snowBallCollider.isTrigger = true;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement) && !_isPlayerBall)
        {
          other.transform.TryGetComponent<Health>(out Health health);
          health.Damage();
        }

        if (other.transform.TryGetComponent<EnemyAiMovement>(out EnemyAiMovement enemyAiMovement) && _isPlayerBall)
        {
            other.transform.TryGetComponent<Health>(out Health health);
            health.Damage();
        }
    }
}
