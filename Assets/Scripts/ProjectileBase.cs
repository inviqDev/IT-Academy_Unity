using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBase : MonoBehaviour
{
    [SerializeField] private float launchForce = 50f;
    [SerializeField] private float lifeTime = 2f;
    
    private ProjectilesSpawner _spawner;
    private Rigidbody _projectileRb;
    private float _currentLifeTime;

    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if (!player.TryGetComponent<ProjectilesSpawner>(out var projectilesSpawner))
        {
            Debug.LogWarning("Projectile spawner not found");
            return;
        }
        
        _spawner = projectilesSpawner;
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime >= 0) return;

        MoveProjectileBackToQueue();
    }

    private protected void MoveProjectileBackToQueue()
    {
        if (!_spawner)
        {
            Debug.LogWarning("Projectile Spawner is not found");
            return;
        }

        _currentLifeTime = 0;
        _spawner.EnqueueProjectile(gameObject);
    }

    private protected void LaunchProjectile()
    {
        _projectileRb ??= GetComponent<Rigidbody>();

        var direction = transform.parent.forward;
        transform.parent = null;

        _currentLifeTime = lifeTime;
        LaunchProjectileInParticularDirection(direction);
    }

    private void LaunchProjectileInParticularDirection(Vector3 direction)
    {
        _projectileRb.linearVelocity = Vector3.zero;
        _projectileRb.angularVelocity = Vector3.zero;

        _projectileRb.AddForce(direction * launchForce, ForceMode.Impulse);
    }
}