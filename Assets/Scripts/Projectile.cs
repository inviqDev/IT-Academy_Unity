using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private ProjectileContainer _projectileContainer;
    private Rigidbody _projectileRb;

    public float launchForce = 12f;
    public float lifeTime = 3f;
    private float _currentLifeTime;

    private void OnEnable()
    {
        _projectileRb ??= GetComponent<Rigidbody>();
        _projectileContainer ??= transform.parent.GetComponent<ProjectileContainer>();
        
        _currentLifeTime = lifeTime;
        var direction = _projectileContainer.GetProjectileLaunchDirection();
        _projectileRb.AddRelativeForce(direction * launchForce, ForceMode.Impulse);
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

        _projectileContainer.PushProjectileToStack(gameObject);
    }
}