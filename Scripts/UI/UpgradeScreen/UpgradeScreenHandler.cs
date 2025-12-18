using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeScreenHandler : MonoBehaviour
{
    public static UpgradeScreenHandler Instance;

    [SerializeField] private GameObject upgradeHolder;
    [SerializeField] private GameObject entryPrefab;

    [Space]
    [SerializeField] private TextMeshProUGUI ageDisplay;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;
    [SerializeField] private TextMeshProUGUI upgradeInfo;
    [SerializeField] private TextMeshProUGUI upgradeHowTo;

    [Header("Upgrade Icons")]
    public Sprite speedIcon;
    public Sprite healthIcon;
    public Sprite damageIcon;
    public Sprite shieldIcon;

    [Header("Upgrade Info")]
    [TextArea(2, 10)] public string speedInfo;
    [TextArea(2, 10)] public string healthInfo;
    [TextArea(2, 10)] public string damageInfo;
    [TextArea(2, 10)] public string shieldInfo;

    [Space]
    [TextArea(2, 10)] public string shieldHowTo;

    [Space]
    [SerializeField] private Animator crossfade;

    private bool upgradeSelected = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Clear
        foreach (Transform child in upgradeHolder.transform)
        {
            Destroy(child.gameObject);
        }

        ageDisplay.text = GameManager.playerAge.ToString();
        upgradeInfo.text = "";
        upgradeHowTo.text = "";

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

        switch (upgrade.upgradeType)
        {
            case UpgradeType.Speed:
                upgradeInfo.text = speedInfo;
                upgradeHowTo.text = "";
                break;

            case UpgradeType.Damage:
                upgradeInfo.text = damageInfo;
                upgradeHowTo.text = "";
                break;

            case UpgradeType.Health:
                upgradeInfo.text = healthInfo;
                upgradeHowTo.text = "";
                break;

            case UpgradeType.Shield:
                if (upgrade.specialInfo != "") upgradeInfo.text = upgrade.specialInfo;
                else upgradeInfo.text = shieldInfo;
                upgradeHowTo.text = shieldHowTo;
                break;
        }
    }

    private void OnExitHover()
    {
        ageDisplay.text = GameManager.playerAge.ToString();
        ageDisplay.color = normalColor;
    }

    private void OnUpgrade(Upgrade selected)
    {
        if (upgradeSelected) return;
        upgradeSelected = true;

        // Apply age
        GameManager.playerAge += selected.ageCost;

        // Apply correct upgrade type
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

            case UpgradeType.Shield:
                GameManager.playerShield += selected.modifier;
                break;
        }

        if (GameManager.level.cutscene != null)
        {
            StartCoroutine(LoadCutscene());
        }
        else
        {
            StartCoroutine(LoadScene("01Battle"));
        }
    }

    private IEnumerator LoadCutscene()
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        GameManager.ShowCutscene();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        PlayerPrefs.SetInt("Gamemanager_playerSpeed", GameManager.playerSpeed);
        PlayerPrefs.SetInt("Gamemanager_playerDamage", GameManager.playerDamage);
        PlayerPrefs.SetInt("Gamemanager_playerHealth", GameManager.playerHealth);
        PlayerPrefs.SetInt("Gamemanager_playerShield", GameManager.playerShield);
        PlayerPrefs.SetInt("Gamemanager_playerAge", GameManager.playerAge);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName);
    }
}
