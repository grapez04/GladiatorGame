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


    private void Start()
    {
        // Get current level
        Level level = GameManager.levels.levels[GameManager.levels.currentLevel -1];

        // Get abilities from current level
        Upgrades cUpgrades = level.upgrades;

        foreach (Upgrade upgrade in cUpgrades.upgrades)
        {
            AddEntry(upgrade);
        }
    }

    private void AddEntry(Upgrade upgrade)
    {
        GameObject newEntry = Instantiate(entryPrefab, upgradeHolder.transform);
        UpgradeEntry entry = newEntry.GetComponent<UpgradeEntry>();

        // Give it the upgrade and the callback
        entry.Setup(upgrade, OnUpgrade);
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
