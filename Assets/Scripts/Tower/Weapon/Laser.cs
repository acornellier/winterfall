using UnityEngine;

public class Laser : MachineGun
{
    [SerializeField] LineRenderer lineRenderer;

    public override void TickFire(Enemy target)
    {
        base.TickFire(target);

        lineRenderer.gameObject.SetActive(true);
        lineRenderer.SetPosition(0, lineRenderer.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);
    }
}