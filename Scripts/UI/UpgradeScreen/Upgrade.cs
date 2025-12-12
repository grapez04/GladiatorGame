using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public UpgradeType upgradeType;
    public float modifier;
    public float ageCost;
}

public enum UpgradeType
{
    Speed,
    Health,
    Damage
}
