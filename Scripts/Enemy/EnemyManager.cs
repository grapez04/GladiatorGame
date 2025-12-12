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
    public VFXHandler vFXHandler;
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

        stateHandler.SetSpeed(currentEnemy.speed);
        enemyStats.maxHealth = currentEnemy.health;
        attackHandler.attackDamage = currentEnemy.attackDamage;
        attackHandler.chargeSpeed = currentEnemy.chargeSpeed;
        attackHandler.attackCooldown = currentEnemy.attackCooldown;

        enemyStats.Init();
    }
}
