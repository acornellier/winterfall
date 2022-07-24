using UnityEngine;

public class Laser : MonoBehaviour, IWeapon
{
    [SerializeField] LineRenderer lineRenderer;

    WeaponSettings _settings;
    float _timeUntilNextDamage;

    public void Initialize(WeaponSettings settings)
    {
        _settings = settings;
    }

    public void TickFire(Enemy target)
    {
        lineRenderer.gameObject.SetActive(true);
        lineRenderer.SetPosition(0, lineRenderer.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);

        if (_timeUntilNextDamage > 0f)
        {
            _timeUntilNextDamage -= Time.deltaTime;
            return;
        }

        target.TakeDamage(_settings.damage);
        _timeUntilNextDamage = 1f / _settings.fireRate;
    }

    public void StopFiring()
    {
        lineRenderer.gameObject.SetActive(false);
    }
}