using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateHandler : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField] private Transform aim;
    [SerializeField] private float stopDistance;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        DistanceFromPlayer();
        RotateAim();
    }

    private void DistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, manager.target.position);

        if (distance > stopDistance)
        {
            // Move toward the player
            manager.agent.SetDestination(manager.target.position);
            manager.agent.isStopped = false;
        }
        else
        {
            // Stop when close enough
            manager.agent.isStopped = true;
            manager.agent.ResetPath();
        }
    }

    private void RotateAim()
    {
        Vector2 direction = (manager.target.position - aim.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        aim.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
