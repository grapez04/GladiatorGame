using NUnit.Framework;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreenHandler : MonoBehaviour
{
    [SerializeField] private GameObject upgradeHolder;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private Sprite[] upgradeTypeIcons;

    [Space]
    [SerializeField] private TextMeshProUGUI ageDisplay;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;

    private void Start()
    {
        ageDisplay.text = GameManager.playerAge.ToString();

        // Get current level
        Level level = GameManager.levels.levels[GameManager.levels.currentLevel - 1];

        // Get abilities from current level
        Upgrades cUpgrades = level.upgrades;

        // Shuffle the upgrades
        Upgrade[] shuffledUpgrades = cUpgrades.upgrades.OrderBy(u => Random.value).ToArray();

        // Instantiate entries in shuffled order
        foreach (Upgrade upgrade in shuffledUpgrades)
        {
            AddEntry(upgrade);
        }
    }

    private void AddEntry(Upgrade upgrade)
    {
        GameObject newEntry = Instantiate(entryPrefab, upgradeHolder.transform);
        UpgradeEntry entry = newEntry.GetComponent<UpgradeEntry>();
        entry.Setup(upgrade, OnUpgrade, OnHoverUpgrade, OnExitHover);
    }

    private void OnHoverUpgrade(Upgrade upgrade)
    {
        ageDisplay.text = (GameManager.playerAge + upgrade.ageCost).ToString();
        ageDisplay.color = hoverColor;
    }

    private void OnExitHover()
    {
        ageDisplay.text = GameManager.playerAge.ToString();
        ageDisplay.color = normalColor;
    }

    private void OnUpgrade(Upgrade selected)
    {
        // Apply age
        GameManager.playerAge += selected.ageCost;

        // Apply  correct upgrade type
        switch (selected.upgradeType)
        {
            case UpgradeType.Speed:
                GameManager.playerSpeed += selected.modifier;
                break;

            case UpgradeType.Damage:
                GameManager.playerDamage += selected.modifier;
                break;

            case UpgradeType.Health:
                GameManager.playerHealth += selected.modifier;
                break;
        }

        GameManager.RestartGame();

    }
}
