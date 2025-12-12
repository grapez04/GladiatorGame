using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeEntry : MonoBehaviour
{
    public Button selectButton;

    [Header("Upgrade Properties")]
    [SerializeField] private TextMeshProUGUI modifierText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private UpgradeType type;

    private Upgrade upgradeData;
    private Action<Upgrade> onClick;

    public void Setup(Upgrade upgrade, Action<Upgrade> onClicked)
    {
        upgradeData = upgrade;
        onClick = onClicked;

        // Type
        type = upgrade.upgradeType;

        // Modifirer
        modifierText.text = $"+ {upgrade.modifier} {type}";

        // Cost
        costText.text = $"- {upgrade.ageCost} Years";

        selectButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(upgradeData);
        });
    }
}
