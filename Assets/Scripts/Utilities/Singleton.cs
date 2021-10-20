using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
  // The static reference to the instance
  public static T Instance { get { return instance; } protected set { instance = value; } }

  // Gets whether an instance of this singleton exists
  public static bool InstanceExists
  {
    get { return instance != null; }
  }

  private static T instance;

  // Awake method to associate singleton with instance
  protected virtual void Awake()
  {
    if (InstanceExists)
    {
      Destroy(gameObject);
    }
    else
    {
      instance = (T)this;
    }
  }

  // OnDestroy method to clear singleton association
  protected virtual void OnDestroy()
  {
    if (InstanceExists == this)
    {
      instance = null;
    }
  }
}


