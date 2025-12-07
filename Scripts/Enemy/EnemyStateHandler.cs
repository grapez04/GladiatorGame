using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateHandler : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField] private Transform aim;
    public float stopDistance;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        DistanceFromPlayer();
        RotateAim();
        Flip();
    }

    private void DistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, manager.target.transform.position);

        if (manager.enemyAttackHandler.isAttacking)
        {
            manager.agent.isStopped = true;
            manager.isMoving = false;
            manager.agent.ResetPath();
            return; // exit early
        }

        if (distance > stopDistance)
        {
            // Move toward the player
            manager.agent.SetDestination(manager.target.transform.position);
            manager.agent.isStopped = false;
            manager.isMoving = true;
        }
        else
        {
            // Stop when close enough
            manager.agent.isStopped = true;
            manager.isMoving = false;
            manager.agent.ResetPath();

            manager.enemyAttackHandler.TryAttackOnce();
        }
    }

    private void RotateAim()
    {
        Vector2 direction = (manager.target.transform.position - aim.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        aim.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Flip()
    {
        if (manager.target.transform.position.x < transform.position.x)
        {
            // Cursor is to the right -> not flipped
            manager.spriteRenderer.flipX = false;
        }
        else
        {
            // Cursor is to the left -> flipped
            manager.spriteRenderer.flipX = true;
        }
    }
}
