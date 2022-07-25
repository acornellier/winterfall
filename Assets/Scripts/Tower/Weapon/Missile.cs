using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionSystem;

    Vector3 _missileDirection;
    WeaponSettings _weaponSettings;
    LayerMask _terrainMask;
    LayerMask _enemyMask;

    bool _exploded;
    readonly Collider[] _castResults = new Collider[16];

    public void Initialize(Enemy target, WeaponSettings weaponSettings)
    {
        _missileDirection = (target.transform.position - transform.position).normalized;
        _weaponSettings = weaponSettings;

        _terrainMask = 1 << LayerMask.NameToLayer("Terrain");
        _enemyMask = 1 << LayerMask.NameToLayer("Enemy");

        transform.rotation = Quaternion.LookRotation(_missileDirection);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _weaponSettings.range);
    }

    void Update()
    {
        if (_exploded) return;

        var distanceThisFrame = _weaponSettings.bulletSpeed * Time.deltaTime;

        var position = transform.position;
        var size = Physics.OverlapSphereNonAlloc(
            position,
            distanceThisFrame * 10,
            _castResults,
            _terrainMask
        );

        if (size == 0)
        {
            transform.Translate(_missileDirection.normalized * distanceThisFrame, Space.World);
            return;
        }

        _exploded = true;

        var newSystem = Instantiate(explosionSystem, position, Quaternion.identity);
        newSystem.transform.localScale = Vector3.one * _weaponSettings.explosionRadius;

        size = Physics.OverlapSphereNonAlloc(
            position,
            _weaponSettings.explosionRadius,
            _castResults,
            _enemyMask
        );

        for (var i = 0; i < size; ++i)
        {
            var enemy = _castResults[i].GetComponent<Enemy>();
            enemy.TakeDamage(_weaponSettings.damage);
        }

        Destroy(gameObject);
    }
}