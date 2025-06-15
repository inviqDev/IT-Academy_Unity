using System.Collections.Generic;
using UnityEngine;

public class ProjectileContainer : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform crosshair;
    
    private Stack<GameObject> _projectiles;
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;

    public int ProjectilesCount { get; private set; } = 20;

    private void Start()
    {
        if (ProjectilesCount <= 0)
        {
            Debug.Log("Projectile count is less than 1");
            return;
        }
        
        _projectiles = new Stack<GameObject>();
        _defaultPosition = transform.position;
        _defaultRotation = transform.rotation;
        var projectileContainer = transform;
        
        for (var i = 0; i < ProjectilesCount; i++)
        {
            var newProjectile = Instantiate(projectilePrefab, projectileContainer);
            PushProjectileToStack(newProjectile);
        }
    }

    public Vector3 GetProjectileLaunchDirection()
    {
        return crosshair.transform.forward;
    }

    public void LaunchNextProjectile(Vector3 direction)
    {
        if (_projectiles.Count < 0) return;

        var projectile = _projectiles.Pop();
        projectile.gameObject.SetActive(true);
    }

    public void PushProjectileToStack(GameObject projectile)
    {
        projectile.SetActive(false);
        projectile.transform.position = _defaultPosition;
        projectile.transform.rotation = _defaultRotation;

        _projectiles.Push(projectile);
    }
}