using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    private EnemyManager manager;

    public bool isAttacking = false;
    public bool isCharging = false;
    public bool isOnCooldown = false; // NEW: cooldown motion freeze

    private bool hasAttacked = false;
    private float attackTimer = 0f;

    [Space]
    [Header("Enemy Combat")]
    public float attackCooldown = 0.5f;
    public float attackDamage = 1;
    public float chargeSpeed = 10f;
    public Vector3 attackSnapshotPos;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        // Cooldown countdown
        if (isOnCooldown)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                // Cooldown finished
                isOnCooldown = false;
                hasAttacked = false;
                isAttacking = false;
                isCharging = false;

                // Reset target
                attackSnapshotPos = Vector3.zero;


                // Restore normal speed
                manager.stateHandler.SetSpeed(manager.currentEnemy.speed);
                manager.agent.isStopped = false;

                Debug.Log("Cooldown finished ? movement restored");
            }

            return; // STOP here: no other attacking logic during cooldown
        }
    }

    public void TryAttackOnce()
    {
        // Don't attack if charging or cooling down
        if (hasAttacked || isCharging || isOnCooldown)
            return;

        manager.agent.isStopped = true;
        manager.isMoving = false;

        Attack();

        hasAttacked = true;
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
        attackTimer = attackCooldown;

        // Stop movement during cooldown
        manager.agent.isStopped = true;
        manager.isMoving = false;
    }

    public void Attack()
    {
        isAttacking = true;
        manager.enemyAnimator.PlayTargetAnim("Attack");
    }

    // Called by animation event
    public void Charge()
    {
        Debug.Log("CHARGE!");

        // Charge toward player
        attackSnapshotPos = manager.player.transform.position;

        isCharging = true;

        manager.agent.isStopped = false;
        manager.isMoving = true;

        // Set charge speed
        manager.stateHandler.SetSpeed(chargeSpeed);

        // Once charge is done, we start cooldown (in EnemyStateHandler)
    }

    public void TryDealDamage()
    {
        Debug.Log("Damage");
        manager.player.healthManager.TakeDamage(attackDamage);
    }

    public void FinishCharge()
    {
        // Called from StateHandler when charge ends
        isCharging = false;
        StartCooldown();
    }
}
