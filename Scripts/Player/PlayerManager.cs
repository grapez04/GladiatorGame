using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerMovement movement;
    public PlayerAnimatorManager animatorManager;
    public PlayerHealthManager healthManager;
    public PlayerAttackHandler attackHandler;
    public MouseHandler mouseHandler;
    public ShieldHandler shieldHandler;
    public VFXHandler vFXHandler;
    public SFXHandler sFXHandler;
    public SpriteRenderer spriteRenderer;

    [Space]
    public PlayerUI playerUI;

    [Space]
    public AgeLevel[] ageLevels;
    public float currentAge;

    private void SetStats()
    {
        healthManager.SetHealth(stats.health);
        movement.movespeed = stats.speed;
        attackHandler.attackDamage = stats.attackDamage;
        attackHandler.radius = stats.attackRange;
        currentAge = stats.age;

        ApplyAgeLevel();
        // health
        // something about movement
        // longer attack cooldown, smaller attack buffer
        // focus window decreases
    }

    public void StartBattle()
    {
        SetStats();
        playerUI.SetUI(stats);
    }

    private void ApplyAgeLevel()
    {
        AgeLevel level = GetAgeLevelForAge(currentAge);
        spriteRenderer.sprite = level.sprite;
        animatorManager.Init(level.animator);
        Debug.Log("Current age: " + level.name);
    }

    private AgeLevel GetAgeLevelForAge(float age)
    {
        foreach (var level in ageLevels)
        {
            if (age >= level.minAge && age <= level.maxAge)
                return level;
        }
        return null;
    }

    [System.Serializable]
    public class AgeLevel
    {
        public string name;

        public float minAge;
        public float maxAge;

        public Sprite sprite;
        public RuntimeAnimatorController animator;
    }
}
