using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class PlaceTowerButton : MonoBehaviour
{
    [SerializeField] Tower tower;

    [Inject] readonly TowerPlacer towerPlacer;

    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => towerPlacer.SetTowerToPlace(tower));
    }

    void OnValidate()
    {
        if (tower != null)
            gameObject.name = tower.name + " Button";
    }
}