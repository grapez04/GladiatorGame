using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayCounter;
    [SerializeField] private TextMeshProUGUI ageCounter;

    [SerializeField] private Transform heartsHolder;
    [SerializeField] private GameObject heartIcon;

    [SerializeField] private Transform shieldsHolder;
    [SerializeField] private GameObject shieldIcon;

    private readonly List<ShieldUI> shields = new();

    public void SetUI(PlayerStats stats)
    {
        Debug.Log("Set UI");

        dayCounter.text = $"Day: {GameManager.levels.currentLevel + 1}";
        ageCounter.text = $"Age: {stats.age}";

        HandleHearts(stats);
        HandleShields(stats);
    }

    public void HandleHearts(PlayerStats stats)
    {
        foreach (Transform child in heartsHolder)
            Destroy(child.gameObject);

        for (int i = 0; i < (int)stats.health; i++)
            Instantiate(heartIcon, heartsHolder);
    }

    public void HandleShields(PlayerStats stats)
    {
        // Clear existing shields (same as hearts)
        foreach (Transform child in shieldsHolder)
            Destroy(child.gameObject);

        shields.Clear();

        // Recreate shields based on stats
        for (int i = 0; i < stats.shield; i++)
        {
            ShieldUI shield = Instantiate(shieldIcon, shieldsHolder)
                .GetComponent<ShieldUI>();

            shields.Add(shield);
        }
    }

    public ShieldUI GetNextAvailableShield()
    {
        foreach (var shield in shields)
        {
            if (shield.IsFull)
                return shield;
        }

        return null;
    }
}
