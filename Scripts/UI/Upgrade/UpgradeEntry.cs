using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeEntry : MonoBehaviour
{
    public Button selectButton;

    public Action<Abillity> onSelected;

    private void Start()
    {
        selectButton.onClick.AddListener(() =>
        {
            //onSelected?.Invoke();
        });
    }
}
