using System.Collections;
using UnityEngine;
using LINQ;

public class EnemySpawner : MonoBehaviour
{
    [Header("Eksternal refrences")]
    public GameObject enemyPrefab;
    public Enemy[] enemies;
    public Transform[] spawnPositions;

    [Header("Spawner settings")]
    public float spawnRate = 1.0f;
    public int enemyCountsForBattle[];
    public int maxEnenemysInBattle = 20;
    [Space]
    public int enemysInBattle = 0;

    void Start()
    {
        GameManager.enemyDied += EnemyDied;
        StartCoroutine(StartBattle());
    }

    public IEnumerator StartBattle()
    {
        while (enemyCountForBattle != 0)
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
        int enemyIndex = Random.Range(0, enemies.length - 1);
        GameObject enemyClone = Instantiate(enemyPrefab);
        enemyClone.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length - 1)].position;
        EnemyManager enemyManager = enemyClone.GetComponent<EnemyManager>();
        enemyManager.currentEnemy = enemies[enemyIndex];
        enemyManager.SetStats();
        enemyClone.SetActive(true);

        enemyCountsForBattle[enemyIndex]--;
        enemysInBattle++;
    }

    public void EnemyDied()
    {
        enemysInBattle--;
        if (enemyCountsForBattle.Sum() == 0 && enemysInBattle == 0)
        {
            GameManager.Upgrade();
        }
    }
}
