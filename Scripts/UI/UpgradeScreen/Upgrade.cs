using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Upgrade
{
    public UpgradeType upgradeType;
    public int modifier;
    public int ageCost;
    public string howTo;
}

public enum UpgradeType
{
    Speed,
    Health,
    Damage,
    Shield
}
