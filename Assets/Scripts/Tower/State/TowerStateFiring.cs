using Zenject;

public class TowerStateFiring : TowerState
{
    public TowerStateFiring(Tower tower) : base(tower)
    {
    }

    public override void Tick()
    {
        Tower.UpdateTarget();

        var target = Tower.EnemyDetector.Target;
        if (target == null)
            return;

        Tower.LookAt(target.transform.position);
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public class Factory : PlaceholderFactory<Tower, TowerStateFiring>
    {
    }
}