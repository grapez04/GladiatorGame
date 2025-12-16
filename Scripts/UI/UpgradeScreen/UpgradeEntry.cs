using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class UpgradeEntry : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button selectButton;

    [Header("Upgrade Properties")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI modifierText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private UpgradeType type;

    private Upgrade upgradeData;
    private Action<Upgrade> onClick;
    private Action<Upgrade> onHoverEnter;
    private Action onHoverExit;

    public void Setup(Upgrade upgrade, Action<Upgrade> clickCallback, Action<Upgrade> hoverEnter = null, Action hoverExit = null)
    {
        upgradeData = upgrade;
        onClick = clickCallback;
        onHoverEnter = hoverEnter;
        onHoverExit = hoverExit;

        // Type
        type = upgrade.upgradeType;

        // Icon
        SetIcon(upgrade);

        // Modifier
        modifierText.text = $"+ {upgrade.modifier} {type}";

        // Cost
        costText.text = $"{upgrade.ageCost} Years";

        selectButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(upgradeData);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHoverEnter?.Invoke(upgradeData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onHoverExit?.Invoke();
    }

    public void SetIcon(Upgrade upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeType.Speed:
                icon.sprite = UpgradeScreenHandler.Instance.speedIcon;
                break;

            case UpgradeType.Health:
                icon.sprite = UpgradeScreenHandler.Instance.healthIcon;
                break;

            case UpgradeType.Damage:
                icon.sprite = UpgradeScreenHandler.Instance.damageIcon;
                break;

            case UpgradeType.Shield:
                icon.sprite = UpgradeScreenHandler.Instance.shieldIcon;
                break;
        }
    }
}
