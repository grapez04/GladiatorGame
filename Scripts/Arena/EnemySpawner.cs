using System.Collections;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [Header("Eksternal refrences")]
    public GameObject enemyPrefab;
    public Enemy[] enemies;
    public Transform[] spawnPositions;

    [Header("Spawner settings")]
    public float spawnRate = 1.0f;
    public int[] enemyCountsForBattle;
    public int maxEnenemysInBattle = 20;
    [Space]
    public int enemysInBattle = 0;

    private int[] _enemyCountsForBattle;

    void Start()
    {
        GameManager.enemyDied += EnemyDied;
    }

    public IEnumerator StartBattle()
    {
        _enemyCountsForBattle = enemyCountsForBattle;
        while (_enemyCountsForBattle.Sum() != 0)
        {
            if (enemysInBattle < maxEnenemysInBattle)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnEnemy()
    {
        Enemy[] spawnableEnemies = enemies.Where((x, i) => _enemyCountsForBattle[i] != 0).ToArray();

        int enemyIndex = Random.Range(0, spawnableEnemies.Length - 1);
        GameObject enemyClone = Instantiate(enemyPrefab);
        enemyClone.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        EnemyManager enemyManager = enemyClone.GetComponent<EnemyManager>();
        enemyManager.currentEnemy = spawnableEnemies[enemyIndex];
        enemyManager.SetEnemy();
        enemyClone.SetActive(true);

        _enemyCountsForBattle[System.Array.IndexOf(enemies, spawnableEnemies[enemyIndex])]--;
        enemysInBattle++;
    }

    public void EnemyDied()
    {
        enemysInBattle--;
        if (_enemyCountsForBattle.Sum() == 0 && enemysInBattle == 0)
        {
            GameManager.Upgrade();
        }
    }
}
