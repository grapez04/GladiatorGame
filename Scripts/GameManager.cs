using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Abilities))]
public class GameManager : MonoBehaviour
{
    public delegate void EnemyDied();
    public static EnemyDied enemyDied;

    public static Abilities abilities;

    [Header("Player stats")]
    public static float playerSpeed = 3f;
    public static float playerDamage = 1f;
    public static float playerHealth = 1f;
    public static float playerRange = 1f;

    private static bool setPlayerStats = false;

    private void Awake()
    {
        abilities = GetComponent<Abilities>();
    }

    private void Start()
    {
        //StartGame();
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

                FindAnyObjectByType<PlayerManager>().StartBattle();
            }
        }
    }

    public static void StartGame()
    {
        playerSpeed =  PlayerPrefs.GetFloat("Gamemanager_playerSpeed", playerSpeed);
        playerDamage = PlayerPrefs.GetFloat("Gamemanager_playerDamage", playerDamage);
        playerHealth = PlayerPrefs.GetFloat("Gamemanager_playerHealth", playerHealth);
        playerRange =  PlayerPrefs.GetFloat("Gamemanager_playerRange", playerRange);

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

        StartGame();
    }
    public static void Upgrade()
    {
        SceneManager.LoadScene("02Upgrades");
    }
}
