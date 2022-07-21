using UnityEngine;
using Zenject;

public class TowerPlacer : ITickable
{
    [Inject] Camera _mainCamera;

    readonly LayerMask _terrainMask;

    Tower _currentPlacingTower;

    TowerPlacer()
    {
        _terrainMask = 1 << LayerMask.NameToLayer("Terrain");
    }

    public void Tick()
    {
        if (ReferenceEquals(_currentPlacingTower, null))
            return;

        var camray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(camray, out var hitInfo, 100f, _terrainMask))
            return;

        _currentPlacingTower.transform.position = hitInfo.point;

        if (ReferenceEquals(hitInfo.collider.gameObject, null))
            return;
        
        // if (!_currentPlacingTower.CheckPlacement(~_terrainMask))
            // return;

        if (!Input.GetMouseButtonDown(0))
            return;

        // _currentPlacingTower.Place();
        _currentPlacingTower = null;
    }

    public void SetTowerToPlace(Tower tower)
    {
        if (!ReferenceEquals(_currentPlacingTower, null))
        {
            Object.Destroy(_currentPlacingTower);
        }
        
        var gameObject = Object.Instantiate(tower);
        _currentPlacingTower = gameObject.GetComponent<Tower>();
    }
}