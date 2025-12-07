using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField] private Transform aim;
    public float stopDistance;

    public float chargeSpeed = 20f;

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

        // If charging ? DO NOT OVERRIDE the charge movement
        if (manager.attackHandler.isCharging)
        {
            manager.agent.SetDestination(manager.target.transform.position);
            return;
        }

        // If in attack animation (before charge)
        if (manager.attackHandler.isAttacking)
        {
            manager.agent.isStopped = true;
            manager.agent.ResetPath();
            manager.isMoving = false;
            return;
        }

        // Normal chasing behavior
        if (distance > stopDistance)
        {
            manager.agent.SetDestination(manager.target.transform.position);
            manager.agent.isStopped = false;
            manager.isMoving = true;
        }
        else
        {
            // Stop and initiate attack
            manager.agent.ResetPath();
            manager.agent.isStopped = true;
            manager.isMoving = false;

            manager.attackHandler.TryAttackOnce();
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
            manager.spriteRenderer.flipX = true;
        else
            manager.spriteRenderer.flipX = false;
    }

    public void SetSpeed(float _speed)
    {
        manager.agent.speed = _speed;
    }
}
