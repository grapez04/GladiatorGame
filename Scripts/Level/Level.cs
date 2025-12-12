using UnityEngine;

[System.Serializable]
public class Level : MonoBehaviour
{
    [SerializeField] public Upgrades upgrades;
    [SerializeField] public Enemy[] enemies;
    [SerializeField] public int[] enemyCounts;
    [SerializeField] public int maxEnemysOnScreen;
    [SerializeField] public float enemySpawnRate;
}
