using UnityEngine;

public class MachineGun : MonoBehaviour, IWeapon
{
    WeaponSettings _settings;
    float _timeUntilNextDamage;

    public virtual void Initialize(WeaponSettings settings)
    {
        _settings = settings;
    }

    public virtual void TickFire(Enemy target)
    {
        if (_timeUntilNextDamage > 0f)
        {
            _timeUntilNextDamage -= Time.deltaTime;
            return;
        }

        target.TakeDamage(_settings.damage);
        _timeUntilNextDamage = 1f / _settings.fireRate;
    }
}