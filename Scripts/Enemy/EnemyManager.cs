using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Enemy currentEnemy; // WHAT ENEMY THIS IS

    [Space]

    public EnemyStats enemyStats;
    public EnemyStateHandler stateHandler;
    public EnemyAnimatorManager enemyAnimator;
    public EnemyAttackHandler attackHandler;
    public NavMeshAgent agent;
    public SpriteRenderer spriteRenderer;

    public bool isMoving;

    public PlayerManager player;
    public Transform currentTarget;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerManager>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void SetEnemy()
    {
        spriteRenderer.sprite = currentEnemy.sprite;
        enemyAnimator.animator.runtimeAnimatorController = currentEnemy.controller;
        enemyAnimator.Initialize();

        // enemy specific stats
        stateHandler.stopDistance = currentEnemy.stopDistance;

        enemyStats.attackDamage = currentEnemy.attackDamage;
        enemyStats.speed = currentEnemy.speed;
        enemyStats.health = currentEnemy.health;

        ApplyStats();
    }

    public void ApplyStats()
    {
        attackHandler.attackDamage = enemyStats.attackDamage;
        stateHandler.SetSpeed(enemyStats.speed);
        enemyStats.maxHealth = enemyStats.health; enemyStats.Init();
    }
}
