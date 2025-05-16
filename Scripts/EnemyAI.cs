using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private float detectionAngle = 120f;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float directionChangeInterval = 3f;

    private Transform player;
    private Vector3 movementDirection;
    private float timeSinceDirectionChange;
    private bool chasingPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickRandomDirection();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            chasingPlayer = true;
        }

        if (chasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = player.position - transform.position;

        if (dirToPlayer.magnitude > detectionRadius)
            return false;

        float angle = Vector3.Angle(transform.forward, dirToPlayer.normalized);
        if (angle > detectionAngle / 2f)
            return false;

        if (Physics.Raycast(transform.position, dirToPlayer.normalized, out RaycastHit hit, detectionRadius, obstacleLayer))
        {
            return hit.collider.CompareTag("Player");
        }

        return true;
    }

    void Patrol()
    {
        timeSinceDirectionChange += Time.deltaTime;

        if (timeSinceDirectionChange >= directionChangeInterval)
        {
            PickRandomDirection();
            timeSinceDirectionChange = 0f;
        }

        transform.Translate(movementDirection * patrolSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), 0.1f);
    }

    void PickRandomDirection()
    {
        Vector3 randomDir = Random.insideUnitSphere;
        randomDir.y = 0;
        movementDirection = randomDir.normalized;
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * chaseSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);
    }
}
