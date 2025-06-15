using UnityEngine;

public class BasicProjectile : ProjectileBase
{
    private void OnEnable()
    {
        LaunchProjectile();
    }

    private void OnCollisionEnter(Collision other)
    {
        MoveProjectileBackToQueue();
    }
}
