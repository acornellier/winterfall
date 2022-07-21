using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(BoxCollider), typeof(NavMeshObstacle), typeof(Outline))]
public class Tower : MonoBehaviour
{
    public BoxCollider Collider { get; private set; }
    public NavMeshObstacle NavMeshObstacle { get; private set; }
    public Outline Outline { get; private set; }

    [Inject] public TowerStatePlacing.Factory TowerStatePlacingFactory;
    [Inject] public TowerStateIdle.Factory TowerStateIdleFactory;

    TowerState _state;

    void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        Outline = GetComponent<Outline>();
        NavMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    void Start()
    {
        SetState(TowerStatePlacingFactory.Create(this));
    }

    void Update()
    {
        _state.Tick();
    }

    public void SetState(TowerState state)
    {
        _state?.OnExit();
        _state = state;
        _state.OnEnter();
    }
    
    public class Factory : PlaceholderFactory<Tower, Tower>
    {
    }
}
