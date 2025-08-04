using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    private Dictionary<string, Queue<IPoolable>> _pool;

    protected override void Awake()
    {
        base.Awake();

        _pool = new Dictionary<string, Queue<IPoolable>>();
    }

    public T GetObjectFromPool<T>(T prefab) where T : MonoBehaviour, IPoolable
    {
        _pool ??= new Dictionary<string, Queue<IPoolable>>();

        if (!_pool.TryGetValue(prefab.UniquePoolKey, out var queue))
        {
            queue = new Queue<IPoolable>();
            _pool[prefab.UniquePoolKey] = queue;
        }
        
        var item = queue.Count > 0 ? queue.Dequeue() : Instantiate(prefab);
        (item as MonoBehaviour)?.gameObject.SetActive(true);

        return (T)item;
    }

    public void ReturnObjectToPool(IPoolable returningItem)
    {
        if (returningItem is null) return;

        var poolingItem = returningItem as MonoBehaviour;

        poolingItem?.gameObject.transform.SetParent(transform);
        poolingItem?.gameObject.SetActive(false);
        
        var itemUniqueType = returningItem.UniquePoolKey;
        if (!_pool.TryGetValue(itemUniqueType, out var queue))
        {
            queue = new Queue<IPoolable>();
            _pool[itemUniqueType] = queue;
        }

        queue.Enqueue(returningItem);
    }
}