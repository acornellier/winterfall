using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(BoxCollider), typeof(NavMeshObstacle), typeof(Outline))]
public class Tower : MonoBehaviour
{
    StateMachine _stateMachine;

    [Inject] TowerStateIdle.Factory _towerStateIdleFactory;
    [Inject] TowerStatePlacing.Factory _towerStatePlacingFactory;

    public BoxCollider Collider { get; private set; }
    public NavMeshObstacle NavMeshObstacle { get; private set; }
    public Outline Outline { get; private set; }
    public bool Placed { get; private set; }

    void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        Outline = GetComponent<Outline>();
        NavMeshObstacle = GetComponent<NavMeshObstacle>();

        _stateMachine = new StateMachine();

        var placing = _towerStatePlacingFactory.Create(this);
        var idle = _towerStateIdleFactory.Create(this);

        _stateMachine.AddTransition(placing, idle, () => Placed);
    }

    void Start()
    {
        _stateMachine.SetState(_towerStatePlacingFactory.Create(this));
    }

    void Update()
    {
        print("Update");
        _stateMachine.Tick();
    }

    public void Place()
    {
        Placed = true;
    }

    public class Factory : PlaceholderFactory<Tower, Tower>
    {
    }
}