using UnityEngine;

public class PlayerStats : Entity
{
    public float attackRange = 2f;

    public PlayerStats(float _health = 1f, float _attackDamage = 1f, float _speed = 1f, int _age = 20)
    {
        health = _health;
        attackDamage = _attackDamage;
        speed = _speed;
        age = _age;
    }
}