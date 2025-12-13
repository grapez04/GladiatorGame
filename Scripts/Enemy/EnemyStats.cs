using UnityEngine;

public class EnemyStats : Entity
{
    [SerializeField] private EnemyManager manager;

    [Header("Enemy Health")]
    public float maxHealth = 2f;
    [SerializeField] private float currentHealth;

    public void Init()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        manager.enemyAnimator.PlayTargetAnim("Hit");

        manager.vFXHandler.PlayOnDamageVFX();

        Debug.Log("Ouch");

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.enemyDied.Invoke();

        // Play death animation
        Destroy(gameObject);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
