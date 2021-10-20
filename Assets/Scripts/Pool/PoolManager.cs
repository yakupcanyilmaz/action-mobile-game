using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
  [SerializeField] List<Poolable> poolables;
  protected Dictionary<Poolable, Pool<Poolable>> pools;

  public Poolable GetPoolable(Poolable poolablePrefab)
  {
    if (!pools.ContainsKey(poolablePrefab))
    {
      pools.Add(poolablePrefab, new Pool<Poolable>(poolablePrefab, Initialize, poolablePrefab.initialPoolCapacity));
    }

    Pool<Poolable> pool = pools[poolablePrefab];
    Poolable spawnedInstance = pool.Get();

    spawnedInstance.pool = pool;
    return spawnedInstance;
  }

  protected void Start()
  {
    pools = new Dictionary<Poolable, Pool<Poolable>>();

    foreach (var poolable in poolables)
    {
      if (poolable == null)
      {
        continue;
      }
      pools.Add(poolable, new Pool<Poolable>(poolable, Initialize, poolable.initialPoolCapacity));
    }
  }

  void Initialize(Component poolable)
  {
    poolable.transform.SetParent(transform, false);
  }
}
