using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class UpgradeEntry : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button selectButton;

    [Header("Upgrade Properties")]
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

        // Modifirer
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
}
