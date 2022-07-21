using UnityEngine;

[CreateAssetMenu(fileName = "New TowerData", menuName = "Create TowerData")]
public class TowerData : ScriptableObject
{
    public Tower prefab;
    public KeyCode shortcut;
    public TowerSettings settings;
}