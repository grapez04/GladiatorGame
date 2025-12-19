using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Upgrade
{
    public UpgradeType upgradeType;
    public int modifier;
    public int ageCost;
    [TextArea(1, 10)] public string specialInfo;
}

public enum UpgradeType
{
    Speed,
    Health,
    Damage,
    Shield
}
