using UnityEngine;
using Zenject;

public class TowerStatePlacing : TowerState
{
    readonly Camera _mainCamera;
    
    readonly LayerMask _terrainMask;

    public TowerStatePlacing(
        Tower tower, 
        [Inject(Id="Main")]
        Camera mainCamera) : base(tower)
    {
        _mainCamera = mainCamera;
        _terrainMask = 1 << LayerMask.NameToLayer("Terrain");
    }

    public override void OnEnter()
    {
        Tower.Collider.isTrigger = true;
        Tower.NavMeshObstacle.enabled = false;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void Tick()
    {
        var camray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(camray, out var hitInfo, 100f, _terrainMask))
            return;

        Tower.transform.position = hitInfo.point;

        if (ReferenceEquals(hitInfo.collider.gameObject, null))
            return;
        
        var bounds = Tower.Collider.bounds;
        var boxCenter = bounds.center;
        var halfExtents = bounds.size / 2;

        var placeable = !Physics.CheckBox(
            boxCenter,
            halfExtents,
            Quaternion.identity,
            ~_terrainMask,
            QueryTriggerInteraction.Ignore
        );
        
        Tower.Outline.OutlineWidth = placeable ? 0 : 10;

        if (!placeable || !Input.GetMouseButtonDown(0))
            return;

        Tower.SetState(Tower.TowerStateIdleFactory.Create(Tower));
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
