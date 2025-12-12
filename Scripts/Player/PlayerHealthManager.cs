using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;

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
        manager.vFXHandler.PlayOnDamageVFX();

        Debug.Log("Player was hurt");

        currentHealth -= damage;

        // Update UI
        manager.stats.health = currentHealth;
        manager.playerUI.SetUI(manager.stats);

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
