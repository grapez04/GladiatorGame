using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    private EnemyManager manager;

    public bool isAttacking = false;
    public bool isCharging = false;

    private bool hasAttacked = false;

    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer = 0f;

    [Space]
    public float attackDamage = 1;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        // cooldown after attack
        if (hasAttacked)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                // Reset attack state
                isAttacking = false;
                isCharging = false;
                hasAttacked = false;

                // Reset speed back to normal
                manager.stateHandler.SetSpeed(manager.currentEnemy.speed);

                Debug.Log("Attack Reset");
            }
        }
    }

    public void TryAttackOnce()
    {
        if (!hasAttacked)
        {
            manager.agent.isStopped = true;
            manager.isMoving = false;

            Attack();

            hasAttacked = true;
            attackTimer = attackCooldown;
        }
    }

    public void Attack()
    {
        isAttacking = true;
        manager.enemyAnimator.PlayTargetAnim("Attack");
    }

    // Called by animation event
    public void DealDamage()
    {
        Debug.Log("CHARGE!");

        // Start charge
        isCharging = true;

        manager.agent.isStopped = false;
        manager.isMoving = true;

        // Set charge speed
        manager.stateHandler.SetSpeed(manager.stateHandler.chargeSpeed);

        // Charge toward player's current position
        manager.agent.SetDestination(manager.target.transform.position);
    }
}
