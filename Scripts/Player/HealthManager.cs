using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Player Health")]
    public float maxHealth = 2f;
    [SerializeField] private float currentHealth;

    public void TakeDamage(int damage)
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
        GameManager.Restart();
    }
}
