using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(BoxCollider), typeof(NavMeshObstacle), typeof(Outline))]
public class Tower : MonoBehaviour
{
    StateMachine<TowerState> stateMachine;

    [Inject] TowerStateIdle.Factory towerStateIdleFactory;
    [Inject] TowerStatePlacing.Factory towerStatePlacingFactory;

    public BoxCollider Collider { get; private set; }
    public NavMeshObstacle NavMeshObstacle { get; private set; }
    public Outline Outline { get; private set; }
    public bool Placed { get; private set; }

    void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        Outline = GetComponent<Outline>();
        NavMeshObstacle = GetComponent<NavMeshObstacle>();

        stateMachine = new StateMachine<TowerState>();

        var placing = towerStatePlacingFactory.Create(this);
        var idle = towerStateIdleFactory.Create(this);

        stateMachine.AddTransition(placing, idle, () => Placed);
    }

    void Start()
    {
        stateMachine.SetState(towerStatePlacingFactory.Create(this));
    }

    void Update()
    {
        stateMachine.Tick();
    }

    public void Place()
    {
        Placed = true;
    }

    public class Factory : PlaceholderFactory<Tower, Tower>
    {
    }
}