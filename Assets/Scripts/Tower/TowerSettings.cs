using System;

[Serializable]
public class TowerSettings
{
    public string name;
    public string description;
    public WeaponSettings weapon = new();
}