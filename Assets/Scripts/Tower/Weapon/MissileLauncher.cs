using UnityEngine;

public class MissileLauncher : MonoBehaviour, IWeapon
{
    [SerializeField] Missile missilePrefab;
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

        var missile = Instantiate(missilePrefab);
        missile.transform.position = muzzle.transform.position;
        missile.Initialize(target, _settings);

        _timeUntilNextShot = 1f / _settings.fireRate;
    }

    public void StopFiring()
    {
    }
}