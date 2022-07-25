using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        var distanceThisFrame = _weaponSettings.bulletSpeed * Time.deltaTime;
        if (direction.magnitude > distanceThisFrame)
        {
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            return;
        }

        _target.TakeDamage(_weaponSettings.damage);
        Destroy(gameObject);
    }
}