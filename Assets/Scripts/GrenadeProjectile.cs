using UnityEngine;

public class GrenadeProjectile : ProjectileBase
{
    [SerializeField] private float explosionRadius = 12f;
    [SerializeField] private float explosionForce = 5000;
    [SerializeField] private LayerMask affectedLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Movable")) return;
        
        ExplodeProjectile();
        MoveProjectileBackToQueue();
    }

    private void ExplodeProjectile()
    {
        var explosionPoint = transform.position;
        
        var affectedObjects = Physics.OverlapSphere(explosionPoint, explosionRadius, affectedLayer);
        foreach (var affectedObject in affectedObjects)
        {
            if (!affectedObject.TryGetComponent(out Rigidbody objectRb)) return;
            objectRb.AddExplosionForce(explosionForce, explosionPoint, explosionRadius);
        }
    }
}
