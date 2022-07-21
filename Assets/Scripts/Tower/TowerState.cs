public abstract class TowerState : IState
{
    protected readonly Tower Tower;

    protected TowerState(Tower tower)
    {
        Tower = tower;
    }

    public abstract void Tick();
    public abstract void OnEnter();
    public abstract void OnExit();
}