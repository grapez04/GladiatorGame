using System.Linq;
using UnityEngine;

public class UpgradeScreenHandler : MonoBehaviour
{
    [SerializeField] private GameObject upgradeHolder;
    [SerializeField] private GameObject entryPrefab;

    private Abillity[] abillities;

    private void Start()
    {
        // Get current level
        Level level = GameManager.levels.levels[GameManager.levels.currentLevel];
        System.Random rnd = new System.Random();

        // Get abilities from current level
        Abilities cAbilities = level.abilities;

        Abillity abillity = cAbilities.abillities[GameManager.levels.currentLevel];

        foreach (Abillity.Attribute attribute in abillity.attributes)
        {
            AddEntry(attribute.modifier, attribute.modifier);
        }
    }

    private void AddEntry(float ability, float age)
    {
        Debug.Log("added an entry!");
    }

    private void RenderEntries(Abillity[] renderAbillities = null)
    {
        abillities = renderAbillities;

        Abillity abillity = abillities[0];
        abillity.attributes[0].modifier = 1;
    }

    private void OnUpgrade(Abillity selected)
    {
        foreach (var attribute in selected.attributes)
        {
            switch (attribute.name)
            {
                case "Speed":
                    GameManager.playerSpeed += attribute.modifier;
                    break;

                case "Damage":
                    GameManager.playerDamage += attribute.modifier;
                    break;

                case "Health":
                    GameManager.playerHealth += attribute.modifier;
                    break;

                case "Age":
                    GameManager.playerAge += attribute.modifier;
                    break;
            }
        }

        // Close UI & restart game
        gameObject.SetActive(false);
        GameManager.RestartGame();
    }
}
