using UnityEngine;

public class EnemyDetector
{
    public Enemy Target { get; private set; }

    readonly LayerMask _enemyMask;
    readonly Collider[] _castResults = new Collider[16];

    public EnemyDetector()
    {
        _enemyMask = 1 << LayerMask.NameToLayer("Enemy");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateTarget(Vector3 position, int range)
    {
        if (Target != null && Vector3.Distance(Target.transform.position, position) < range)
            return;

        var size = Physics.OverlapSphereNonAlloc(
            position,
            range,
            _castResults,
            _enemyMask
        );

        if (size > 0)
            Target = _castResults[0].GetComponent<Enemy>();
    }
}