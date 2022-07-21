using UnityEngine;
using Zenject;

public class TowerPlacer
{
    [Inject] readonly Tower.Factory _towerFactory;

    Tower _currentPlacingTower;

    public void SetTowerToPlace(TowerData towerData)
    {
        if (_currentPlacingTower is { Placed: false, })
            Object.Destroy(_currentPlacingTower.gameObject);

        _currentPlacingTower = _towerFactory.Create(towerData.prefab, towerData.settings);
    }
}