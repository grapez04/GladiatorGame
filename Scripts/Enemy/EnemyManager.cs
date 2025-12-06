using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public EnemyStats enemyStats;

    public Transform target;
    public NavMeshAgent agent;

    public bool isMoving = false;

    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();

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
