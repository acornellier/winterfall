using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class PlaceTowerButton : MonoBehaviour
{
    [SerializeField] TowerData towerData;

    [Inject] readonly TowerPlacer _towerPlacer;

    Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlaceTower);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            PlaceTower();
    }

    void OnValidate()
    {
        if (towerData != null)
            gameObject.name = towerData.details.name + " Button";
    }

    void PlaceTower()
    {
        _towerPlacer.SetTowerToPlace(towerData);
    }
}