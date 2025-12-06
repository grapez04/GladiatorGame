using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
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

        SetStats();
    }

    private void SetStats()
    {
        //enemyAttackHandler.radius = stats.defaultAttackRange;
    }
}
