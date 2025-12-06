using UnityEngine;

public class PlayerStats : Entity
{
    public float defaultMoveSpeed = 5f;
    public float defaultAttackRange = 1f;

    public PlayerStats(float _health = 1f, float _attackDamage = 1f, float _speed = 1f, float _defaultMoveSpeed = 5f, float _defaultAttackRange = 1f)
    {
        health = _health;
        attackDamage = _attackDamage;
        speed = _speed;
        defaultMoveSpeed = _defaultMoveSpeed;
        defaultAttackRange = _defaultAttackRange;
    }
}