using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Player Health")]
    public float maxHealth = 1f;
    [SerializeField] private float currentHealth;

    public void SetHealth(float newHealth)
    {
        maxHealth = newHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player was hurt");

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.RestartGame();
    }
}
