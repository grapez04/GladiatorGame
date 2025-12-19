using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeScreenHandler : MonoBehaviour
{
    public static UpgradeScreenHandler Instance;

    [SerializeField] private GameObject upgradeHolder;
    [SerializeField] private GameObject entryHolder;
    [SerializeField] private GameObject entryPrefab;

    [Space]
    [SerializeField] private TextMeshProUGUI ageDisplay;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;
    [SerializeField] private TextMeshProUGUI upgradeInfo;
    [SerializeField] private TextMeshProUGUI upgradeHowTo;
    [SerializeField] private TextMeshProUGUI controlText;

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

    [Header("Introduction")]
    [TextArea(2, 10)] public string[] merchantIntroductionText;
    private int currentIntroductionIndex = 0;

    [TextArea(2, 10)]
    [SerializeField] private string defaultUpgradeText = "Please pick an upgrade";

    private float typingSpeed = 0.01f;
    private Coroutine typingCoroutine;

    private bool upgradeSelected = false;
    private bool introIsActive = false;

    [Space]
    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip memoryTheme;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        controlText.text = "";

        if (GameManager.levels.currentLevel == 1)
        {
            introIsActive = true;
            currentIntroductionIndex = 0;

            TypeUpgradeText(merchantIntroductionText[currentIntroductionIndex]);
            upgradeHowTo.text = "Click to continue";
            upgradeHolder.SetActive(false);
            return;
        }

        introIsActive = false;
        Setup();
    }

    public void Continue()
    {
        if (!introIsActive) return;

        // Advance intro text
        if (currentIntroductionIndex < merchantIntroductionText.Length - 1)
        {
            currentIntroductionIndex++;
            TypeUpgradeText(merchantIntroductionText[currentIntroductionIndex]);
            return;
        }

        // Last line reached = lock input forever
        introIsActive = false;
        upgradeHowTo.text = "";
        upgradeHolder.SetActive(true);

        ShowDefaultUpgradeText();
        Setup();
    }

    private void ShowDefaultUpgradeText()
    {
        if (introIsActive) return;
        TypeUpgradeText(defaultUpgradeText);
    }

    private void TypeUpgradeText(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeTextRoutine(text));
    }

    private IEnumerator TypeTextRoutine(string text)
    {
        upgradeInfo.text = "";

        if (!introIsActive)
            upgradeHowTo.text = "";

        foreach (char c in text)
        {
            upgradeInfo.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    private void Setup()
    {
        // Clear existing upgrades
        foreach (Transform child in entryHolder.transform)
        {
            Destroy(child.gameObject);
        }

        ageDisplay.text = GameManager.playerAge.ToString();
        upgradeInfo.text = "";
        upgradeHowTo.text = "";
        controlText.text = "";

        // Get current level
        Level level = GameManager.levels.levels[GameManager.levels.currentLevel - 1];
        Upgrades cUpgrades = level.upgrades;

        // Shuffle upgrades
        Upgrade[] shuffledUpgrades = cUpgrades.upgrades.OrderBy(u => Random.value).ToArray();

        // Instantiate entries
        foreach (Upgrade upgrade in shuffledUpgrades)
        {
            AddEntry(upgrade);
        }

        ShowDefaultUpgradeText();
    }

    private void AddEntry(Upgrade upgrade)
    {
        GameObject newEntry = Instantiate(entryPrefab, entryHolder.transform);
        UpgradeEntry entry = newEntry.GetComponent<UpgradeEntry>();
        entry.Setup(upgrade, OnUpgrade, OnHoverUpgrade, OnExitHover);
    }

    private void OnHoverUpgrade(Upgrade upgrade)
    {
        ageDisplay.text = (GameManager.playerAge + upgrade.ageCost).ToString();
        ageDisplay.color = hoverColor;
        string _confirm = "Click to confirm";

        switch (upgrade.upgradeType)
        {
            case UpgradeType.Speed:
                TypeUpgradeText(speedInfo);
                upgradeHowTo.text = _confirm;
                controlText.text = "";
                break;
            case UpgradeType.Damage:
                TypeUpgradeText(damageInfo);
                upgradeHowTo.text = _confirm;
                controlText.text = "";
                break;
            case UpgradeType.Health:
                TypeUpgradeText(healthInfo);
                upgradeHowTo.text = _confirm;
                controlText.text = "";
                break;
            case UpgradeType.Shield:
                TypeUpgradeText(string.IsNullOrEmpty(upgrade.specialInfo) ? shieldInfo : upgrade.specialInfo);
                upgradeHowTo.text = _confirm;
                controlText.text = shieldHowTo;
                break;
        }
    }

    private void OnExitHover()
    {
        ageDisplay.text = GameManager.playerAge.ToString();
        ageDisplay.color = normalColor;

        upgradeHowTo.text = "";
    }

    private void OnUpgrade(Upgrade selected)
    {
        if (upgradeSelected) return;
        upgradeSelected = true;

        // Apply age
        GameManager.playerAge += selected.ageCost;

        // Apply upgrade
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

        musicSource.Stop();
        musicSource.clip = memoryTheme;
        musicSource.Play();
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
