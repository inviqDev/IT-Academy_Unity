using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private ProjectileContainer _projectileContainer;
    private Rigidbody _projectileRb;
    
    public float launchForce = 50f;
    public float lifeTime = 2f;
    private float _currentLifeTime;

    private void OnEnable()
    {
        _projectileRb ??= GetComponent<Rigidbody>();
        _projectileContainer ??= transform.parent.GetComponent<ProjectileContainer>();

        if (!_projectileContainer)
        {
            Debug.LogError("ProjectileContainer not found");
            return;
        }
        
        var direction = transform.parent.forward;
        transform.parent = null;
        
        LaunchProjectileInParticularDirection(direction);
        _currentLifeTime = lifeTime;
    }

    private void LaunchProjectileInParticularDirection(Vector3 direction)
    {
        _projectileRb.linearVelocity = Vector3.zero;
        _projectileRb.angularVelocity = Vector3.zero;
        
        _projectileRb.AddForce(direction * launchForce, ForceMode.Impulse);
    }

    private void Update()
    {
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime > 0) return;

        if (!_projectileContainer)
        {
            Debug.Log("Projectile Container not found");
            return;
        }

        _projectileContainer.EnqueueProjectile(gameObject);
    }
}