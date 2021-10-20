using System;
using System.Collections.Generic;
using UnityEngine;
// using Object = UnityEngine.Object;

public class Pool<T> where T : Component
{
  protected T prefab;
  protected readonly Action<T> initialize;

  protected readonly List<T> available;
  protected readonly List<T> all;

  public Pool(T prefab, Action<T> initialize, int initalCapacity)
  {
    this.prefab = prefab;
    this.initialize = initialize;
    this.available = new List<T>();
    this.all = new List<T>();

    if (initalCapacity > 0)
    {
      Grow(initalCapacity);
    }
  }

  T PrefabFactory()
  {
    T newElement = GameObject.Instantiate(prefab);
    if (initialize != null)
    {
      initialize(newElement);
    }
    return newElement;
  }

  public virtual T Get()
  {
    if (available.Count == 0)
    {
      Grow(1);
    }

    int itemIndex = available.Count - 1;
    T item = available[itemIndex];
    available.RemoveAt(itemIndex);

    item.gameObject.SetActive(true);

    return item;
  }

  public void Grow(int amount)
  {
    for (int i = 0; i < amount; i++)
    {
      AddNewElement();
    }
  }

  protected virtual T AddNewElement()
  {
    T newElement = PrefabFactory();
    all.Add(newElement);
    available.Add(newElement);

    newElement.gameObject.SetActive(false);

    return newElement;
  }

  public virtual void Return(T pooledItem)
  {
    if (all.Contains(pooledItem) && !available.Contains(pooledItem))
    {
      ReturnToPoolInternal(pooledItem);
    }
  }

  protected virtual void ReturnToPoolInternal(T element)
  {
    available.Add(element);

    element.gameObject.SetActive(false);
  }
}


// public class Pool<T> where T : Component
// {
//   protected Func<T> factory;
//   protected readonly Action<T> reset;
//   protected readonly List<T> available;
//   protected readonly List<T> all;

//   public Pool(Func<T> factory, Action<T> reset, int initalCapacity)
//   {
//     this.available = new List<T>();
//     this.all = new List<T>();
//     this.factory = factory;
//     this.reset = reset;

//     if (initalCapacity > 0)
//     {
//       Grow(initalCapacity);
//     }
//   }

//   public virtual T Get()
//   {
//     return Get(reset);
//   }

//   public virtual T Get(Action<T> resetOverride)
// 	{
// 		if (available.Count == 0)
// 		{
// 			Grow(1);
// 		}

// 		int itemIndex = available.Count - 1;
// 		T item = available[itemIndex];
// 		available.RemoveAt(itemIndex);

// 		if (resetOverride != null)
// 		{
// 			resetOverride(item);
// 		}

// 		return item;
// 	}

//   public void Grow(int amount)
//   {
//     for (int i = 0; i < amount; i++)
//     {
//       AddNewElement();
//     }
//   }

//   protected virtual T AddNewElement()
//   {
//     T newElement = factory();
//     all.Add(newElement);
//     available.Add(newElement);

//     return newElement;
//   }

//   public virtual void Return(T pooledItem)
//   {
//     if (all.Contains(pooledItem) && !available.Contains(pooledItem))
//     {
//       ReturnToPoolInternal(pooledItem);
//     }
//   }

//   protected virtual void ReturnToPoolInternal(T element)
//   {
//     available.Add(element);
//   }

//   protected static T DummyFactory()
// 	{
// 		return default(T);
// 	}
// }

// public class UnityComponentPool<T> : Pool<T> where T : Component
// {

// 	public UnityComponentPool(Func<T> factory, Action<T> reset, int initialCapacity)
// 	: base(factory, reset, initialCapacity)
// 	{
// 	}

//   public override T Get(Action<T> resetOverride)
//   {
//     T element = base.Get(resetOverride);
//     element.gameObject.SetActive(true);
//     return element;
//   }

//   protected override void ReturnToPoolInternal(T element)
//   {
//     element.gameObject.SetActive(false);
//     base.ReturnToPoolInternal(element);
//   }

//   protected override T AddNewElement()
//   {
//     T newElement = base.AddNewElement();
//     newElement.gameObject.SetActive(false);
//     return newElement;
//   }
// }

// public class AutoComponentPrefabPool<T> : UnityComponentPool<T> where T : Component
// {
//   protected readonly T prefab;
//   protected readonly Action<T> initialize;

//   public AutoComponentPrefabPool(T prefab, Action<T> initialize, Action<T> reset, int initialCapacity) : base(DummyFactory, reset, 0)
//   {
//     this.prefab = prefab;
//     this.initialize = initialize;
//     this.factory = PrefabFactory;
//     if (initialCapacity > 0)
//     {
//       Grow(initialCapacity);
//     }
//   }

//   T PrefabFactory()
//   {
//     T newElement = Object.Instantiate(prefab);
//     if (initialize != null)
//     {
//       initialize(newElement);
//     }
//     return newElement;
//   }
// }
