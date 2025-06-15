using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    [SerializeField] private GameObject basicProjectilePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!other.TryGetComponent<ProjectilesSpawner>(out var spawner))
        {
            Debug.Log("The Projectiles Spawner component was not found");
            return;
        }
        
        spawner.SpawnBasicProjectilesPool(basicProjectilePrefab);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!other.TryGetComponent<ProjectilesSpawner>(out var spawner))
        {
            Debug.Log("The Projectiles Spawner component was not found");
            return;
        }
        
        spawner.DestroySpawnedProjectiles();
    }
}