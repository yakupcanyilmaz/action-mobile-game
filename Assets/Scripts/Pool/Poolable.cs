using UnityEngine;
using UnityEngine.AddressableAssets;

public class Poolable : MonoBehaviour
{
  public int initialPoolCapacity = 1;

  public Pool<Poolable> pool;

  protected virtual void Repool()
  {
    transform.SetParent(PoolManager.Instance.transform, false);
    pool.Return(this);
  }

  public static void TryPool(GameObject gameobject)
  {
    var poolable = gameobject.GetComponent<Poolable>();
    if (poolable != null && poolable.pool != null && PoolManager.InstanceExists)
    {
      poolable.Repool();
    }
    else
    {
      Destroy(gameobject);
    }
  }

  public static T TryGetPoolable<T>(GameObject prefab) where T : Component
  {
    var poolable = prefab.GetComponent<Poolable>();
    T instance = poolable != null && PoolManager.InstanceExists ? PoolManager.Instance.GetPoolable(poolable).GetComponent<T>() : Instantiate(prefab).GetComponent<T>();
    return instance;
  }

  // public static T TryGetPoolable<T>(GameObject prefab) where T : Component
  // {
  //   var poolable = prefab.GetComponent<Poolable>();
  //   if (poolable != null && PoolManager.InstanceExists)
  //   {
  //     return PoolManager.Instance.GetPoolable(poolable).GetComponent<T>();
  //   }
  //   else
  //   {
  //     T instance = null;
  //     var op = Addressables.InstantiateAsync(prefab);
  //     instance = op.Result.GetComponent<T>();
  //     return instance;
  //   }
  // }

  public static GameObject TryGetPoolable(GameObject prefab)
  {
    var poolable = prefab.GetComponent<Poolable>();
    GameObject instance = poolable != null && PoolManager.InstanceExists ? PoolManager.Instance.GetPoolable(poolable).gameObject : Instantiate(prefab);
    return instance;
  }

  // public static GameObject TryGetPoolable(GameObject prefab)
  // {
  //   var poolable = prefab.GetComponent<Poolable>();
  //   if (poolable != null && PoolManager.InstanceExists)
  //   {
  //     return PoolManager.Instance.GetPoolable(poolable).gameObject;
  //   }
  //   else
  //   {
  //     GameObject instance = null;
  //     var op = Addressables.InstantiateAsync(prefab);
  //     instance = op.Result;
  //     return instance;
  //   }
  // }
}
