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
        attackHandler.radius = stats.attackRange;
        currentAge = stats.age;

        // Age Decay:
        ApplyAgeLevel();
        float ageMultiplier = GetAgeSpeedMultiplier();

        movement.movespeed = movement.movespeed * ageMultiplier;
        Debug.Log("Speed is now " + movement.movespeed);

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

    private float GetAgeSpeedMultiplier()
    {
        float maxAge = ageLevels[^1].maxAge;
        float age01 = Mathf.Clamp01(currentAge / maxAge);
        return ageDecayCurve.Evaluate(age01);
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
