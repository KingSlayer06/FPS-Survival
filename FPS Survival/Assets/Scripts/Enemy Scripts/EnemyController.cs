using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE, 
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private EnemyAudio enemyAudio;

    private Transform target;
    private EnemyState enemyState;

    [SerializeField] private float walkSpeed = 0.5f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] public float chaseDistance = 7f;
    [SerializeField] private float attackDistance = 1.8f;
    [SerializeField] private float chaseAfterAttackDistance = 2f;
    [SerializeField] private float waitBeforeAttack = 2f;

    [SerializeField] private float patrolRadiusMin = 20f;
    [SerializeField] private float patrolRadiusMax = 80f;
    [SerializeField] private float patrolTime = 15f;

    [SerializeField] private GameObject attackPoint;

    private float currentChaseDistance;
    private float patrolTimer;
    private float attackTimer;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAudio = GetComponentInChildren<EnemyAudio>();
        target = GameObject.FindWithTag(Tags.PLAYER).transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolTime;
        attackTimer = waitBeforeAttack;
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }

        if (enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    private void Patrol()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;
        if(patrolTimer > patrolTime)
        {
            SetNewDestination();
            patrolTimer = 0f;
        }

        // walk Animation
        if(navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Walk(true);
        }
        else
        {
            enemyAnimator.Walk(false);
        }

        // Distance betwwen Enemy and Player
        if(Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnimator.Walk(false);
            enemyState = EnemyState.CHASE;

            // Player spotted audio
            enemyAudio.ScreamSound();
        }
    }

    private void SetNewDestination()
    {
        float radius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 direction = Random.insideUnitSphere * radius;
        direction += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(direction, out navHit, radius, -1);
        navMeshAgent.SetDestination(navHit.position);
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = runSpeed;

        // set players position as destination
        // Chase the player
        navMeshAgent.SetDestination(target.position);

        // Run Animation
        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Run(true);
        }
        else
        {
            enemyAnimator.Run(false);
        }

        // Attack player when near 
        if(Vector3.Distance(transform.position ,target.position) <= attackDistance)
        {
            enemyAnimator.Walk(false);
            enemyAnimator.Run(false);
            enemyState = EnemyState.ATTACK;

            // reset chase distance
            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }

        // Player ran away from enemy
        else if(Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            // stop running
            enemyAnimator.Run(false);
            enemyState = EnemyState.PATROL;

            // reset patrol timer
            patrolTimer = patrolTime;

            // reset chase distance
            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
    }

    private void Attack()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        attackTimer += Time.deltaTime;

        if(attackTimer > waitBeforeAttack)
        {
            enemyAnimator.Attack();
            attackTimer = 0f;

            // Play attack sound
            enemyAudio.AttackSound();
        }

        // Player runs away then chase him
        if(Vector3.Distance(transform.position, target.position) > 
            (attackDistance + chaseAfterAttackDistance))
        {
            enemyState = EnemyState.CHASE;
        }
    }

    public void AttackPointOn()
    {
        attackPoint.SetActive(true);
    }

    public void AttackPointOff()
    {
        if (attackPoint.activeInHierarchy)
            attackPoint.SetActive(false);
    }

    public EnemyState Enemy_State
    {
        get;set;
    }
}
