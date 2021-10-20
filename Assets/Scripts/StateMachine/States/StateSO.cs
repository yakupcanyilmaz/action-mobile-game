using UnityEngine;

namespace ShooterPrototype.StateMachines.ScriptableObjects
{
  public abstract class StateSO : ScriptableObject
  {
    internal State GetState(Player player)
    {
      var state = CreateState();
      state.Awake(player);
      return state;
    }
    internal State GetState(Agent agent)
    {
      var state = CreateState();
      state.Awake(agent);
      return state;
    }
    protected abstract State CreateState();
  }

  public abstract class StateSO<T> : StateSO where T : State, new()
  {
    protected override State CreateState()
    {
      return new T();
    }
  }
}