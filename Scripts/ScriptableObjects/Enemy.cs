using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy: ScriptableObject
{
    public Sprite sprite;
    public RuntimeAnimatorController controller;
    
    [Space]

    public int attackDamage = 1;
    public float speed = 2;
}
