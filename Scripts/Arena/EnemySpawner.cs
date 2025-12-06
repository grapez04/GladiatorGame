using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Eksternal refrences")]
    public GameObject enemyPrefab;
    public Transform[] spawnPositions;

    [Header("Spawner settings")]
    public float spawnRate = 1.0f;
    public int enemyCountForBattle = 100;
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
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length - 1)].position;
        enemy.SetActive(true);

        enemyCountForBattle--;
        enemysInBattle++;
    }

    public void EnemyDied()
    {
        enemysInBattle--;
        if (enemyCountForBattle == 0 && enemysInBattle == 0)
        {
            GameManager.Upgrade();
        }
    }
}
