public interface IWeapon
{
    void Initialize(WeaponSettings settings);
    void TickFire(Enemy target);
    void StopFiring();
}