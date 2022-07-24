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
        {
            Tower.Weapon.StopFiring();
            return;
        }

        Tower.LookAt(target.transform.position);
        Tower.Weapon.TickFire(target);
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
        Tower.Weapon.StopFiring();
    }

    public class Factory : PlaceholderFactory<Tower, TowerStateFiring>
    {
    }
}