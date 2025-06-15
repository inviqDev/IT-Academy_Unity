using UnityEngine;

public class BasicProjectile : ProjectileBase
{
    private void OnCollisionEnter(Collision other)
    {
        MoveProjectileBackToQueue();
    }
}