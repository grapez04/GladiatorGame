using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerMovement movement;
    public PlayerAttackHandler attackHandler;
    public MouseHandler mouseHandler;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        movement = GetComponent<PlayerMovement>();
        attackHandler = GetComponent<PlayerAttackHandler>();
        mouseHandler = GetComponent<MouseHandler>();

        SetStats();
    }

    private void SetStats()
    {
        movement.movespeed = stats.defaultMoveSpeed;
        attackHandler.radius = stats.defaultAttackRange;
    }
}
