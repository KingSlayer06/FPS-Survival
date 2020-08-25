using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private EnemyController enemyController;
    private NavMeshAgent navMeshAgent;
    private EnemyAudio enemyAudio;

    [SerializeField] private float health = 100f;

    private bool isDead;

    void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyController = GetComponent<EnemyController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAudio = GetComponentInChildren<EnemyAudio>();
    }

    public void EnemyDamage(float damage)
    {
        if (isDead)
            return;

            health -= damage;

        if (health <= 0f)
        {
            isDead = true;
            Dead();
        }

        if (enemyController.Enemy_State == EnemyState.PATROL)
        {
            enemyController.chaseDistance = 50f;
        }
    }

    private void Dead()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        enemyController.enabled = false;
        enemyAnimator.Death();
        StartCoroutine(DeathSound());
    }

    public void EnemyDead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeathSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.DieSound();
    }
}
