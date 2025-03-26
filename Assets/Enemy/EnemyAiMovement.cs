using UnityEngine;
using UnityEngine.AI;

public class EnemyAiMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private NavMeshAgent enemyAgent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float chasePlayerAtSpecificDistance;
    [SerializeField] private EnemyAiSnowAttack enemyAiSnowAttack;
    private bool _isRunning;
    void Update()
    {

        if ((Vector3.Distance(transform.position, playerTransform.position) > chasePlayerAtSpecificDistance )  && !enemyAiSnowAttack.IsDigging()  )
        {
            MoveTowardsPlayerAtSpecificDistane();
            Debug.Log("MoveTowardsPlayerAtSpecificDistance");
        }
        else
        {
            Debug.Log("Stop");
            StopAtPosition();
        }
    }

    private void StopAtPosition()
    {
        if(!_isRunning)return;
        _isRunning = false;
        enemyAgent.SetDestination(transform.position);
    }

    private void MoveTowardsPlayerAtSpecificDistane()
    {
        _isRunning = true;
        enemyAgent.SetDestination(playerTransform.position);
    }

    public bool IsRunning()
    {
        return _isRunning;
    }
}
