using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy: ScriptableObject
{
    public Sprite sprite;
    public RuntimeAnimatorController controller;

    [Space]

    public float stopDistance = 1;
    public int attackDamage = 1;
    public float speed = 2;
    public int health = 1;
}
