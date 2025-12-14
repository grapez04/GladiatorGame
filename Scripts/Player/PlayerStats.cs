using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health = 1f;
    public float attackDamage = 1f;
    public float speed = 1f;
    public int age = 20;
    public int shield = 0;

    public PlayerStats(float _health = 1f, float _attackDamage = 1f, float _speed = 1f, int _age = 20, int _shield = 0)
    {
        health = _health;
        attackDamage = _attackDamage;
        speed = _speed;
        age = _age;
        shield = _shield;
    }
}