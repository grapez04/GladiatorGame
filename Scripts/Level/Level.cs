using UnityEngine;

[System.Serializable]
public class Level : MonoBehaviour
{
    [SerializeField] public Abilities abilities;
    [SerializeField] public Enemy[] enemies;
    [SerializeField] public int[] enemyCountss;
    [SerializeField] public int maxEnemysOnScreen;
    [SerializeField] public float enemySpawnRate;
}
