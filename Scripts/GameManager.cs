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
    public static int playerSpeed = 3;
    public static int playerDamage = 1;
    public static int playerHealth = 1;
    public static int playerShield = 0;
    public static int playerAge = 20;

    private static bool setPlayerStats = false;
    private static bool loadUpgrade = false;
    private static float loadUpgradeTime = 0f;

    private void Awake()
    {
        levels = FindAnyObjectByType<Levels>();
        level = levels.levels[levels.currentLevel];
        StartGame();
    }

    private void Update()
    {
        if (setPlayerStats)
        {
            PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
            EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
            PlayerManager playerManager = FindAnyObjectByType<PlayerManager>();
            if (playerStats != null && enemySpawner != null && playerManager != null)
            {
                setPlayerStats = false;
                playerStats = FindAnyObjectByType<PlayerStats>();
                playerStats.attackDamage = playerDamage;
                playerStats.health = playerHealth;
                playerStats.speed = playerSpeed;
                playerStats.age = playerAge;
                playerStats.shield = playerShield;

                enemySpawner.spawnRate = level.enemySpawnRate;
                enemySpawner.enemyCountsForBattle = (int[])level.enemyCounts.Clone();
                enemySpawner.maxEnenemysInBattle = level.maxEnemysOnScreen;
                enemySpawner.enemies = level.enemies;

                playerManager.StartBattle();
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
        playerSpeed =  PlayerPrefs.GetInt("Gamemanager_playerSpeed", playerSpeed);
        playerDamage = PlayerPrefs.GetInt("Gamemanager_playerDamage", playerDamage);
        playerHealth = PlayerPrefs.GetInt("Gamemanager_playerHealth", playerHealth);
        playerShield =  PlayerPrefs.GetInt("Gamemanager_playerShield", playerShield);
        playerAge = PlayerPrefs.GetInt("Gamemanager_playerAge", playerAge);

        level = levels.levels[levels.currentLevel];

        setPlayerStats = true;

    }
    public static void RestartGame()
    {
        PlayerPrefs.SetInt("Gamemanager_playerSpeed", playerSpeed);
        PlayerPrefs.SetInt("Gamemanager_playerDamage", playerDamage);
        PlayerPrefs.SetInt("Gamemanager_playerHealth", playerHealth);
        PlayerPrefs.SetInt("Gamemanager_playerShield", playerShield);
        PlayerPrefs.SetInt("Gamemanager_playerAge", playerAge);
        PlayerPrefs.Save();
        

        SceneManager.LoadScene("01Battle");
    }
    public static void Upgrade()
    {
        loadUpgrade = true;
    }
}
