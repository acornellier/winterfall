using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Create EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;
    public Enemy.Stats stats;
}