using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    private EnemyManager manager;

    public bool isAttacking = false;
    private bool hasAttacked = false; // prevents multiple attacks
    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer = 0f;

    [Space]
    public int attackDamage = 1;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        if (hasAttacked)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                hasAttacked = false; // Enemy can attack again
            }
        }
    }

    public void TryAttackOnce()
    {
        if (!hasAttacked)
        {
            Attack();
            hasAttacked = true;
            attackTimer = attackCooldown;
        }
    }

    public void Attack()
    {
        isAttacking = true;
        manager.enemyAnimator.PlayTargetAnim("Attack");
        DealDamage();
    }

    public void DealDamage()
    {
        manager.target.healthManager.TakeDamage(attackDamage); // Deal damage to player
    }
}
