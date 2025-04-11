
using System.Collections;
using UnityEngine;


public class EnemyAiSnowAttack : MonoBehaviour
{
    
    [SerializeField] private Transform enemyHand;
    [SerializeField] private float snowCollectionDelay;
    [SerializeField] private float snowBallThrowSpeed;
    
    [SerializeField] private float throwAnimationDelay;
    [SerializeField] private EnemyAiMovement enemyAiMovement;
    [SerializeField] private GameObject player;
 
    [SerializeField] private float attackDistance;
    [SerializeField] private Health enemyHealth;
    private SnowBallFactory _snowBallFactory;
    private SnowBall _snowBall;
    private bool _isThrowingSnow;
    private bool _isDiggingSnow=false;
    private bool _isSnowBallInHand=false;

    private void Start()
    {
        _snowBallFactory = new SnowBallFactory();
    }

    private void Update()
    {
        if (!enemyAiMovement.IsRunning() && !_isDiggingSnow && !_isThrowingSnow)
        {
            DigSnowBall();
        }

        if (_isSnowBallInHand&& !_isThrowingSnow  && Vector3.Distance(transform.position,player.transform.position) <=attackDistance)
        {
            ThrowSnowBall();
        }
    }
    private void ThrowSnowBall()
    {
        if(_snowBall==null)return;
        
        var targetPosition = player.transform.position;
        LookAtTarget(targetPosition);
        
        _isThrowingSnow = true;
        StartCoroutine(ThrowAnimationDelay(targetPosition));
    }
    private void LookAtTarget(Vector3 targetPosition)
    {
        var velocity = TrajectoryUtility.CalculateVelocity(enemyHand.transform.position, targetPosition, snowBallThrowSpeed);

        transform.forward = new Vector3(velocity.normalized.x, 0, velocity.normalized.z);
    }

    private IEnumerator ThrowAnimationDelay(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(throwAnimationDelay/2);
        var velocity = TrajectoryUtility.CalculateVelocity(enemyHand.transform.position, targetPosition, snowBallThrowSpeed);
        transform.forward =  new Vector3(velocity.normalized.x, 0, velocity.normalized.z);
        ThrowSnowBall(velocity);
        yield return new WaitForSeconds(throwAnimationDelay/2);
        _isThrowingSnow =false;
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
        _snowBall= _snowBallFactory.Create(enemyHand, false);
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
