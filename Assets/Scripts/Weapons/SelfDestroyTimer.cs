using UnityEngine;
using UnityEngine.Events;

public class SelfDestroyTimer : MonoBehaviour
{
  public float time = 2f;

  public Timer timer;

  // public UnityEvent death;

  protected virtual void OnEnable()
  {
    if (timer == null)
    {
      timer = new Timer(time, OnTimeEnd);
    }
    else
    {
      timer.Reset();
    }
  }


  protected virtual void Update()
  {
    if (timer == null)
    {
      return;
    }
    timer.Tick(Time.deltaTime);
  }

  protected virtual void OnTimeEnd()
  {
    Poolable.TryPool(gameObject);
    timer.Reset();
  }
}
