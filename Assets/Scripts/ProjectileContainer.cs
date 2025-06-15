using System.Collections.Generic;
using UnityEngine;

public class ProjectileContainer : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform crosshairMarker;

    private Queue<GameObject> _projectiles;

    public int ProjectilesCount { get; private set; } = 30;

    private void Start()
    {
        if (ProjectilesCount <= 0)
        {
            Debug.Log("Projectile count is less than 1");
            return;
        }

        _projectiles = new Queue<GameObject>();

        projectilePrefab.gameObject.SetActive(false);
        for (var i = 0; i < ProjectilesCount; i++)
        {
            var newProjectile = Instantiate(projectilePrefab, transform);
            EnqueueProjectile(newProjectile);
        }
    }

    public void LaunchNextProjectile()
    {
        if (_projectiles.Count < 0 || !_projectiles.TryDequeue(out var projectile)) return;
        
        projectile.gameObject.SetActive(true);
    }

    public void EnqueueProjectile(GameObject projectile)
    {
        projectile.transform.SetParent(transform);
        projectile.transform.position = transform.parent.position;
        projectile.transform.rotation = transform.rotation;

        _projectiles.Enqueue(projectile);
        projectile.SetActive(false);
    }
}