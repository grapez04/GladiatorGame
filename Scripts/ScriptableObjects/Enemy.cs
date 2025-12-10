using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy: ScriptableObject
{
    public Sprite sprite;
    public RuntimeAnimatorController controller;

    [Space]

    public float speed = 2;
    public int health = 1;
    public float stopDistance = 1;
    public int attackDamage = 1;
    public float attackCooldown = 0.5f;
    public float chargeSpeed = 10;
}
