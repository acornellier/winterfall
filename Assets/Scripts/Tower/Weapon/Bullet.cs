using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
    Enemy _target;
    WeaponSettings _weaponSettings;

    public void Initialize(Enemy target, WeaponSettings weaponSettings)
    {
        _target = target;
        _weaponSettings = weaponSettings;
    }

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var direction = _target.transform.position - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;
        if (direction.magnitude <= distanceThisFrame)
        {
            _target.TakeDamage(_weaponSettings.damage);
            Destroy(gameObject);
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
}