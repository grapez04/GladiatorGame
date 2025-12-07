using UnityEngine;

public class EnemyStats : Entity
{
    [Header("Enemy Health")]
    public float maxHealth = 2f;
    [SerializeField] private float currentHealth;


    [Space]
    [SerializeField] private GameObject bloodSplat;

    public void Init()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodSplat, transform.position, Quaternion.identity);

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
