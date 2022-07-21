using Zenject;

public class TowerStateIdle : TowerState
{
    public TowerStateIdle(Tower tower) : base(tower)
    {
    }

    public override void Tick()
    {
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public class Factory : PlaceholderFactory<Tower, TowerStateIdle>
    {
    }
}