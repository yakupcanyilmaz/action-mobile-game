using System;
using UnityEngine.Events;

[Serializable]
public class GameEventListener
{
  public GameEventSO gameEvent;
  public UnityEvent unityEvent;

  // private void OnEnable()
  // {
  //   gameEvent.Register(this);
  // }
  // private void OnDisable()
  // {
  //   gameEvent.Deregister(this);
  // }
  public void RaiseEvent()
  {
    unityEvent.Invoke();
  }
}
