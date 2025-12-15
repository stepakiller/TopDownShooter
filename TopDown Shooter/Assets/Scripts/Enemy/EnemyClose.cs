using UnityEngine;

public class EnemyClose : MonoBehaviour
{
    [Header("Патрулирование")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float waitTime = 2f;
    
    [Header("Обнаружение")]
    public float detectionRange = 5f;
    public float fieldOfView = 90f;
    public LayerMask obstacleLayer;
    
    [Header("Преследование")]
    public float chaseSpeed = 4f;
    public float chaseRange = 8f;
    
    [Header("Ближний бой")]
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    
    private Transform player;
    private UnityEngine.AI.NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private float waitCounter = 0f;
    private float attackCounter = 0f;
    private bool isWaiting = false;
    private Vector3 lastKnownPlayerPosition;
    
    private enum EnemyState { Patrolling, Chasing, Attacking, Searching }
    private EnemyState currentState = EnemyState.Patrolling;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        if (agent == null)
        {
            agent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
        }
        
        agent.speed = patrolSpeed;
    }

    void Update()
    {
        attackCounter -= Time.deltaTime;
        
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                CheckForPlayer();
                break;
                
            case EnemyState.Chasing:
                ChasePlayer();
                CheckAttackRange();
                break;
                
            case EnemyState.Attacking:
                AttackPlayer();
                break;
                
            case EnemyState.Searching:
                SearchForPlayer();
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                isWaiting = false;
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                SetDestination(patrolPoints[currentPatrolIndex].position);
            }
            return;
        }
        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isWaiting = true;
            waitCounter = 0f;
        }
    }

    void SetDestination(Vector3 target)
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target);
        }
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange && CanSeePlayer())
        {
            currentState = EnemyState.Chasing;
            agent.speed = chaseSpeed;
            lastKnownPlayerPosition = player.position;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        
        if (angleToPlayer > fieldOfView / 2)
            return false;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, directionToPlayer, out hit, detectionRange, obstacleLayer))
        {
            if (!hit.transform.CompareTag("Player"))
            {
                return false;
            }
        }
        
        return true;
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (CanSeePlayer())
        {
            lastKnownPlayerPosition = player.position;
            SetDestination(player.position);
        }
        else
        {
            SetDestination(lastKnownPlayerPosition);
            
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentState = EnemyState.Searching;
            }
        }
        
        if (distanceToPlayer > chaseRange)
        {
            currentState = EnemyState.Patrolling;
            agent.speed = patrolSpeed;
            SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void SearchForPlayer()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
        
        if (CanSeePlayer())
        {
            currentState = EnemyState.Chasing;
            agent.speed = chaseSpeed;
            lastKnownPlayerPosition = player.position;
        }
        
        waitCounter += Time.deltaTime;
        if (waitCounter > 5f)
        {
            currentState = EnemyState.Patrolling;
            agent.speed = patrolSpeed;
            waitCounter = 0f;
        }
    }

    void CheckAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= attackRange && CanSeePlayer())
        {
            currentState = EnemyState.Attacking;
            agent.isStopped = true;
        }
    }

    void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0;
        if (directionToPlayer != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }
        
        if (distanceToPlayer > attackRange)
        {
            currentState = EnemyState.Chasing;
            agent.isStopped = false;
            return;
        }
        
        if (attackCounter <= 0)
        {
            PerformAttack();
            attackCounter = attackCooldown;
        }
    }

    void PerformAttack()
    {
        
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null) playerHealth.GetDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.cyan;
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * detectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * detectionRange;
        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);
        
        if (player != null)
        {
            Gizmos.color = CanSeePlayer() ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up, player.position + Vector3.up);
        }
    }
}