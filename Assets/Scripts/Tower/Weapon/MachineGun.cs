using UnityEngine;

public class MachineGun : MonoBehaviour, IWeapon
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform muzzle;

    WeaponSettings _settings;
    float _timeUntilNextShot;

    public void Initialize(WeaponSettings settings)
    {
        _settings = settings;
    }

    public void TickFire(Enemy target)
    {
        if (_timeUntilNextShot > 0f)
        {
            _timeUntilNextShot -= Time.deltaTime;
            return;
        }

        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = muzzle.transform.position;
        bullet.Initialize(target, _settings);

        _timeUntilNextShot = 1f / _settings.fireRate;
    }

    public void StopFiring()
    {
    }
}