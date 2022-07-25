using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class TowerStatePlacing : TowerState
{
    readonly Camera _mainCamera;

    readonly LayerMask _terrainMask;

    public TowerStatePlacing(
        Tower tower,
        [Inject(Id = "Main")] Camera mainCamera) : base(tower)
    {
        _mainCamera = mainCamera;
        _terrainMask = 1 << LayerMask.NameToLayer("Terrain");
    }

    public override void OnEnter()
    {
        Tower.Collider.isTrigger = true;
        Tower.NavMeshObstacle.enabled = false;
    }

    public override void Tick()
    {
        var camray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(camray, out var hitInfo, 100f, _terrainMask))
            return;

        var pos = new Vector3((int)hitInfo.point.x, hitInfo.point.y, (int)hitInfo.point.z);
        Tower.transform.position = pos;

        if (ReferenceEquals(hitInfo.collider.gameObject, null))
            return;

        var bounds = Tower.Collider.bounds;
        var boxCenter = bounds.center;
        var halfExtents = bounds.size / 2 - 0.1f * Vector3.one;

        var placeable = !Physics.CheckBox(
            boxCenter,
            halfExtents,
            Quaternion.identity,
            ~_terrainMask,
            QueryTriggerInteraction.Ignore
        );

        Tower.Outline.OutlineWidth = placeable ? 0 : 10;
        Tower.Outline.OutlineColor = Color.red;

        if (placeable && Input.GetMouseButtonDown(0) &&
            !EventSystem.current.IsPointerOverGameObject())
            Tower.Place();
    }

    public override void OnExit()
    {
        Tower.Collider.isTrigger = false;
        Tower.NavMeshObstacle.enabled = true;
    }

    public class Factory : PlaceholderFactory<Tower, TowerStatePlacing>
    {
    }
}