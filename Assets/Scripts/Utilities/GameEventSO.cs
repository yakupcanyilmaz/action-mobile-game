using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Shooter Prototype/GameEvent", order = 0)]
public class GameEventSO : ScriptableObject
{
  HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();
  public void Invoke()
  {
    foreach (var globalEventListener in listeners)
    {
      globalEventListener.RaiseEvent();
    }
  }
  public void Register(GameEventListener gameEventListener)
  {
    listeners.Add(gameEventListener);
  }
  public void Deregister(GameEventListener gameEventListener)
  {
    listeners.Remove(gameEventListener);
  }
}
