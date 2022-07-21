using UnityEngine;
using Zenject;

public class TowerPlacer
{
    [Inject] readonly Tower.Factory towerFactory;

    Tower currentPlacingTower;

    public void SetTowerToPlace(Tower tower)
    {
        if (currentPlacingTower is { Placed: false, })
            Object.Destroy(currentPlacingTower.gameObject);

        currentPlacingTower = towerFactory.Create(tower);
    }
}