using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayCounter;
    [SerializeField] private TextMeshProUGUI ageCounter;

    [SerializeField] private GameObject heartsHolder;
    [SerializeField] private GameObject heartIcon;

    [SerializeField] private GameObject shieldsHolder;
    [SerializeField] private GameObject shieldIcon;

    public void SetUI(PlayerStats stats)
    {
        print("Set UI");

        dayCounter.text = $"Day: {GameManager.levels.currentLevel + 1}";
        ageCounter.text = $"Age: {stats.age}";

        ageCounter.text = "Age: " + stats.age.ToString();

        HandleHearts(stats);
        HandleShields(stats);
    }

    public void HandleHearts(PlayerStats stats)
    {
        // Clear current hearts
        foreach (Transform child in heartsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        int heartsToSpawn = (int)stats.health;

        // Spawn hearts
        for (int i = 0; i < heartsToSpawn; i++)
        {
            Instantiate(heartIcon, heartsHolder.transform);
        }
    }

    public void HandleShields(PlayerStats stats)
    {
        // Clear current hearts
        foreach (Transform child in shieldsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        int shieldsToSpawn = (int)stats.shield;

        // Spawn hearts
        for (int i = 0; i < shieldsToSpawn; i++)
        {
            Instantiate(shieldIcon, shieldsHolder.transform);
        }
    }
}
