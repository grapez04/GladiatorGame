using UnityEngine;

public class EnemyStats : Entity
{
    [Header("Enemy Health")]
    public float maxHealth = 2f;
    [SerializeField] private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Ouch");

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Here you can add death logic:
        // - Play death animation
        // - Spawn loot
        // - Destroy the enemy object

        Destroy(gameObject);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
