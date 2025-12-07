using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private float sceneLoadWaitTime = 1f;

    public delegate void EnemyDied();
    public static EnemyDied enemyDied;

    public static Levels levels;
    public static Level level;

    [Header("Player stats")]
    public static float playerSpeed = 3f;
    public static float playerDamage = 1f;
    public static float playerHealth = 1f;
    public static float playerRange = 1f;

    private static bool setPlayerStats = false;
    private static bool loadUpgrade = false;
    private static float loadUpgradeTime = 0f;

    private void Awake()
    {
        levels = FindAnyObjectByType<Levels>();
        StartGame();
    }

    private void Update()
    {
        if (setPlayerStats)
        {
            PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
            if (playerStats != null)
            {
                setPlayerStats = false;
                playerStats = FindAnyObjectByType<PlayerStats>();
                playerStats.attackDamage = playerDamage;
                playerStats.health = playerHealth;
                playerStats.speed = playerSpeed;

                EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
                enemySpawner.spawnRate = level.enemySpawnRate;
                enemySpawner.enemyCountsForBattle = (int[])level.enemyCountss.Clone();
                enemySpawner.maxEnenemysInBattle = level.maxEnemysOnScreen;
                enemySpawner.enemies = level.enemies;

                FindAnyObjectByType<PlayerManager>().StartBattle();
                StartCoroutine(enemySpawner.StartBattle());
            }
        }

        if (loadUpgrade)
        {
            if (loadUpgradeTime >= sceneLoadWaitTime)
            {
                loadUpgrade = false;
                loadUpgradeTime = 0;
                levels.currentLevel += 1;
                if (levels.levels.Length <= levels.currentLevel)
                {
                    SceneManager.LoadScene("03Ending");
                }
                else
                {
                    SceneManager.LoadScene("02Upgrades");
                }
            }
            else
            {
                loadUpgradeTime += Time.deltaTime;
            }
        }
    }

    public static void StartGame()
    {
        playerSpeed =  PlayerPrefs.GetFloat("Gamemanager_playerSpeed", playerSpeed);
        playerDamage = PlayerPrefs.GetFloat("Gamemanager_playerDamage", playerDamage);
        playerHealth = PlayerPrefs.GetFloat("Gamemanager_playerHealth", playerHealth);
        playerRange =  PlayerPrefs.GetFloat("Gamemanager_playerRange", playerRange);

        level = levels.levels[levels.currentLevel];

        setPlayerStats = true;

    }
    public static void RestartGame()
    {
        PlayerPrefs.SetFloat("Gamemanager_playerSpeed", playerSpeed);
        PlayerPrefs.SetFloat("Gamemanager_playerDamage", playerDamage);
        PlayerPrefs.SetFloat("Gamemanager_playerHealth", playerHealth);
        PlayerPrefs.SetFloat("Gamemanager_playerRange", playerRange);
        PlayerPrefs.Save();

        SceneManager.LoadScene("01Battle");
    }
    public static void Upgrade()
    {
        loadUpgrade = true;
    }
}
