using System.Collections;
using UnityEngine;

public class SnowAttack : MonoBehaviour
{


    [SerializeField] private Transform playerHand;
    [SerializeField] private float snowCollectionDelay;
    [SerializeField] private float snowballThrowSpeed;
    [SerializeField] private float throwAnimationDelay;
    
    private SnowBallFactory _snowBallFactory;
    private bool _isDiggingSnow = false;
    private bool _isThrowingSnow = false;
    private bool _isSnowBallInHand;
    private SnowBall _snowBall;

    private void OnEnable()
    {
        InputHub.OnScreenContact += ThrowSnowBallOnTapIfPossible;
    }
    
    private void OnDisable()
    {
        InputHub.OnScreenContact -= ThrowSnowBallOnTapIfPossible;
    }
    private void Start()
    {
        _snowBallFactory = new SnowBallFactory();
    }
    
    private void ThrowSnowBallOnTapIfPossible(object sender, InputHub.OnScreenContactEventArgs e)
    {
        if(_snowBall==null)return;
        
        var targetPosition = e.TouchWorldPosition;
        LookAtTarget(targetPosition);
        
        _isThrowingSnow = true;
        StartCoroutine(ThrowAnimationDelay(targetPosition));
    }

    private IEnumerator ThrowAnimationDelay(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(throwAnimationDelay/2);
        var velocity = TrajectoryUtility.CalculateVelocity(playerHand.transform.position, targetPosition, snowballThrowSpeed);
        ThrowSnowBall(velocity);
        yield return new WaitForSeconds(throwAnimationDelay/2);
        _isThrowingSnow =false;
    }
    private void LookAtTarget(Vector3 targetPosition)
    {
        var velocity = TrajectoryUtility.CalculateVelocity(playerHand.transform.position, targetPosition, snowballThrowSpeed);

        transform.forward = new Vector3(velocity.normalized.x, 0, velocity.normalized.z);
    }
    private void ThrowSnowBall(Vector3 velocity)
    {
        _snowBall.ChangeSnowBallInToThrowState(velocity);
        _snowBall = null;
        _isSnowBallInHand = false;
    }


    public void DigSnowBall()
    {
        if(_isSnowBallInHand)return;
        _isDiggingSnow = true;
        StartCoroutine(CreateSnowBallAfterDigging());
    }

    private IEnumerator CreateSnowBallAfterDigging()
    {
        yield return new WaitForSeconds(snowCollectionDelay);
       _snowBall= _snowBallFactory.Create(playerHand, true);
        _isDiggingSnow = false;
        _isSnowBallInHand = true;
    }

    public bool IsDiggingSnow()
    {
        return _isDiggingSnow;
    }

    public bool IsThrowingSnow()
    {
        return _isThrowingSnow;
    }

    public bool IsSnowBallInHand()
    {
        return _isSnowBallInHand;
    }
}

