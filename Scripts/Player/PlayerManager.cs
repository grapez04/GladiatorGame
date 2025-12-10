using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerMovement movement;
    public PlayerAnimatorManager animatorManager;
    public PlayerHealthManager healthManager;
    public PlayerAttackHandler attackHandler;
    public MouseHandler mouseHandler;
    public SFXHandler sFXHandler;

    public SpriteRenderer spriteRenderer;

    public float currentAge;

    //public AgeLevel[] ageLevel;

    private void SetStats()
    {
        healthManager.SetHealth(stats.health);
        movement.movespeed = stats.speed;
        attackHandler.attackDamage = stats.attackDamage;
        attackHandler.radius = stats.attackRange;
        currentAge = stats.age;

        //SetAge(null, null);
    }

    public void StartBattle()
    {
        SetStats();
    }

    private void SetAge(Sprite def, RuntimeAnimatorController controller)
    {
        spriteRenderer.sprite = def;
        animatorManager.Init(controller);
    }
}
