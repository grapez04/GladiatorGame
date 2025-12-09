using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField] private Transform aim;
    private float attackDistance = 1.2f; // Hit range
    public float stopDistance;
    private float snapShotOffset = 0.2f;

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
        float distance = Vector3.Distance(transform.position, manager.player.transform.position);

        // STOP EVERYTHING DURING COOLDOWN
        if (manager.attackHandler.isOnCooldown)
        {
            manager.agent.isStopped = true;
            manager.isMoving = false;
            return;
        }

        // CHARGING STATE
        if (manager.attackHandler.isCharging)
        {
            Vector3 targetPos = manager.attackHandler.attackSnapshotPos;
            Vector3 dir = (targetPos - transform.position).normalized;
            Vector3 chargeTarget = targetPos - dir;

            manager.agent.SetDestination(chargeTarget);

            // Determines when the charge movement ends
            float distanceCharge = Vector3.Distance(transform.position, chargeTarget);

            if (distanceCharge <= 0.2f)
            {
                // Checks whether the player is actually close to the enemy at impact time
                float hitDistance = Vector3.Distance(manager.player.transform.position, transform.position);

                if (Vector3.Distance(transform.position, manager.player.transform.position) <= attackDistance)
                {
                    manager.attackHandler.TryDealDamage();
                }

                manager.agent.ResetPath();
                manager.attackHandler.FinishCharge();
            }

            return;
        }

        // NORMAL CHASING
        if (!manager.attackHandler.isAttacking)
        {
            if (distance > stopDistance)
            {
                manager.agent.SetDestination(manager.player.transform.position);
                manager.agent.isStopped = false;
                manager.isMoving = true;
            }
            else
            {
                manager.agent.ResetPath();
                manager.agent.isStopped = true;
                manager.isMoving = false;

                manager.attackHandler.TryAttackOnce();
            }
        }
    }

    private void RotateAim()
    {
        Vector3 direction = (manager.player.transform.position - aim.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aim.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Flip()
    {
        manager.spriteRenderer.flipX = manager.player.transform.position.x < transform.position.x;
    }

    public void SetSpeed(float _speed)
    {
        manager.agent.speed = _speed;
    }

    private void OnDrawGizmos()
    {
        // Draw attack distance around the enemy
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // Draw stop distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        // Draw the snapshot position if charging
        if (manager != null && manager.attackHandler != null && manager.attackHandler.isCharging)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(manager.attackHandler.attackSnapshotPos, 0.2f); // small debug sphere
        }
    }
}
