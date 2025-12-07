using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField] private Transform aim;
    public float stopDistance;
    public float attackDistance = 1.0f;

    public float chargeSpeed = 15f;

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
            //manager.attackHandler.TryDealDamage();

            Vector3 targetPos = manager.target.transform.position;
            Vector3 dir = (targetPos - transform.position).normalized;
            Vector3 chargeTarget = targetPos - dir * attackDistance;

            manager.agent.SetDestination(chargeTarget);

            // End charge when close enough
            if (distance <= attackDistance + 0.2f)
            {
                manager.agent.ResetPath();
                manager.attackHandler.TryDealDamage();
                manager.attackHandler.FinishCharge();
            }

            return;
        }

        // NORMAL CHASING
        if (!manager.attackHandler.isAttacking)
        {
            if (distance > stopDistance)
            {
                manager.agent.SetDestination(manager.target.transform.position);
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
        Vector3 direction = (manager.target.transform.position - aim.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aim.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Flip()
    {
        manager.spriteRenderer.flipX = manager.target.transform.position.x < transform.position.x;
    }

    public void SetSpeed(float _speed)
    {
        manager.agent.speed = _speed;
    }
}
