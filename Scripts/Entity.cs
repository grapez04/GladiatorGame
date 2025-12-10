using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health = 1f;
    public float attackDamage = 1f;
    public float speed = 1f;
    public int age = 20;

    public Entity(float _health = 1f, float _attackDamage = 1f, float _speed = 1f)
    {
        health = _health;
        attackDamage = _attackDamage;
        speed = _speed;
    }
}
