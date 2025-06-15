using System.Collections.Generic;
using UnityEngine;

public class ProjectilesSpawner : MonoBehaviour
{
    [SerializeField] private Transform projectilesParent;
    [SerializeField] private int spawnAmount = 25;

    private Queue<GameObject> _projectiles;

    public void SpawnBasicProjectilesPool(GameObject projectilePrefab)
    {
        if (!projectilePrefab)
        {
            Debug.Log($"{projectilePrefab.name} is missing !!");
            return;
        }

        projectilePrefab.SetActive(false);
        _projectiles = new Queue<GameObject>();
        
        for (var i = 0; i < spawnAmount; i++)
        {
            var newProjectile = Instantiate(projectilePrefab);
            EnqueueProjectile(newProjectile);
        }
    }

    public void EnqueueProjectile(GameObject projectile)
    {
        if (_projectiles == null)
        {
            Destroy(projectile);
            return;
        }
        
        projectile.transform.SetParent(projectilesParent);
        projectile.transform.position = projectilesParent.position;
        projectile.transform.rotation = projectilesParent.rotation;
        projectile.SetActive(false);

        _projectiles.Enqueue(projectile);
    }
    
    public void LaunchNextProjectile()
    {
        if (_projectiles == null || !_projectiles.TryDequeue(out var projectile)) return;
        projectile.gameObject.SetActive(true);
    }

    public void DestroySpawnedProjectiles()
    {
        if (projectilesParent.childCount < 1) return;
        
        _projectiles = null;
        for (var i = 0; i < projectilesParent.childCount; i++)
        {
            Destroy(projectilesParent.GetChild(i).gameObject);
        }
    }
}