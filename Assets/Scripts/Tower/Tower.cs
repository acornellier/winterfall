using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(IWeapon))]
public class Tower : MonoBehaviour
{
    [SerializeField] GameObject head;

    [Inject] public TowerSettings Settings { get; private set; }
    [Inject] TowerStateIdle.Factory _towerStateIdleFactory;
    [Inject] TowerStatePlacing.Factory _towerStatePlacingFactory;
    [Inject] TowerStateFiring.Factory _towerStateFiringFactory;

    StateMachine _stateMachine;

    public BoxCollider Collider { get; private set; }
    public NavMeshObstacle NavMeshObstacle { get; private set; }
    public Outline Outline { get; private set; }
    public IWeapon Weapon { get; private set; }

    public EnemyDetector EnemyDetector { get; private set; }
    public bool Placed { get; private set; }

    void Awake()
    {
        _stateMachine = new StateMachine();

        Collider = GetComponent<BoxCollider>();
        Outline = GetComponent<Outline>();
        NavMeshObstacle = GetComponent<NavMeshObstacle>();
        Weapon = GetComponent<IWeapon>();
        Weapon.Initialize(Settings.weapon);

        EnemyDetector = new EnemyDetector();

        var placing = _towerStatePlacingFactory.Create(this);
        var idle = _towerStateIdleFactory.Create(this);
        var firing = _towerStateFiringFactory.Create(this);

        _stateMachine.AddTransition(placing, idle, () => Placed);
        _stateMachine.AddTransition(idle, firing, () => EnemyDetector.Target);
        _stateMachine.AddTransition(firing, idle, () => EnemyDetector.Target == null);

        _stateMachine.SetState(placing);
    }

    void Update()
    {
        _stateMachine.Tick();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Settings.weapon.range);
        Gizmos.color = Color.red;
        if (EnemyDetector.Target)
            Gizmos.DrawLine(transform.position, EnemyDetector.Target.transform.position);
    }

    public void Place()
    {
        Placed = true;
    }

    public void LookAt(Vector3 target)
    {
        var targetRotation = new Vector3(target.x, head.transform.position.y, target.z);
        head.transform.LookAt(targetRotation);
    }

    public void UpdateTarget()
    {
        EnemyDetector.UpdateTarget(transform.position, Settings.weapon.range);
    }

    public class Factory : PlaceholderFactory<Tower, TowerSettings, Tower>
    {
    }
}