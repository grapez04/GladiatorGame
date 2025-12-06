using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Enemy currentEnemy; // WHAT ENEMY THIS IS

    [Space]

    public EnemyStats enemyStats;
    public EnemyAnimatorManager enemyAnimator;
    public EnemyAttackHandler enemyAttackHandler;
    public NavMeshAgent agent;
    public SpriteRenderer spriteRenderer;

    public bool isMoving;

    public PlayerManager target;

    private void Awake()
    {
        target = FindAnyObjectByType<PlayerManager>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void SetStats()
    {
        spriteRenderer.sprite = currentEnemy.sprite;
        enemyAnimator.animator.runtimeAnimatorController = currentEnemy.controller;
        enemyAnimator.Initialize();

        // enemy specific stats
        enemyAttackHandler.attackDamage = currentEnemy.attackDamage;
        agent.speed = currentEnemy.speed;
    }
}
