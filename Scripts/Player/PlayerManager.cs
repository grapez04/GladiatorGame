using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerMovement movement;
    public PlayerAnimatorManager animator;
    public PlayerAttackHandler attackHandler;
    public MouseHandler mouseHandler;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        SetStats();
    }

    private void SetStats()
    {
        //healthManager.health = stats.health
        movement.movespeed = stats.speed;
        //attackHandler.attackDamager = stats.attackDamage;
        attackHandler.radius = stats.defaultAttackRange;
    }

    public void StartBattle()
    {

    }
}
