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
        movement.movespeed = stats.defaultMoveSpeed;
        attackHandler.radius = stats.defaultAttackRange;
    }
}
