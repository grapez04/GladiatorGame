using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public UpgradeType upgradeType;
    public int modifier;
    public int ageCost;
}

public enum UpgradeType
{
    Speed,
    Health,
    Damage,
    Shield
}
