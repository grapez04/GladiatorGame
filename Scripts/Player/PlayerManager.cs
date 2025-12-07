using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerMovement movement;
    public PlayerAnimatorManager playerAnimator;
    public PlayerHealthManager healthManager;
    public PlayerAttackHandler attackHandler;
    public MouseHandler mouseHandler;

    public SpriteRenderer spriteRenderer;

    private void SetStats()
    {
        healthManager.SetHealth(stats.health);
        movement.movespeed = stats.speed;
        //attackHandler.attackDamager = stats.attackDamage;
        attackHandler.radius = stats.attackRange;
    }

    public void StartBattle()
    {
        SetStats();
    }
}
