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
    [Header("Aging")]
    public AgeLevel[] ageLevels;
    public float currentAge;
    [SerializeField] private AnimationCurve ageDecayCurve;

    private void SetStats()
    {
        healthManager.SetHealth(stats.health);
        movement.movespeed = stats.speed;
        attackHandler.attackDamage = stats.attackDamage;
        shieldHandler.SetShieldCount(stats.shield);
        currentAge = stats.age;

        // Age Decay:
        ApplyAgeLevel();
        float ageMultiplier = GetAgeMultiplier();

        ApplyAgeAttackCooldown();
        movement.movespeed *= ageMultiplier;
        Debug.Log("Speed is now " + movement.movespeed + ", Cooldown is now " + attackHandler.currentAttackCooldown);

        // attackHandler.radius = stats.attackRange;
        // longer attack cooldown, smaller attack buffer
        // focus window decreases
        // health affect
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

    private float GetAgeMultiplier()
    {
        float maxAge = ageLevels[^1].maxAge;
        float age01 = Mathf.Clamp01(currentAge / maxAge);
        return ageDecayCurve.Evaluate(age01);
    }

    private void ApplyAgeAttackCooldown()
    {
        float maxAge = ageLevels[^1].maxAge;

        float age01 = Mathf.Clamp01(currentAge / maxAge);
        float invertedAge01 = 1f - age01;

        // Curve now means: X = youth ? old age, Y = cooldown multiplier
        float cooldownMultiplier = ageDecayCurve.Evaluate(invertedAge01);

        // button-mash floor
        float minCooldown = 0.03f;

        attackHandler.currentAttackCooldown = Mathf.Max(minCooldown, attackHandler.baseAttackCooldown * cooldownMultiplier);
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
