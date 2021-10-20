using System;
using UnityEngine;

namespace ShooterPrototype.StateMachines.ScriptableObjects
{
  public abstract class ConditionSO : ScriptableObject
  {
    internal Condition GetCondition(Player player)
    {
      var condition = CreateCondition();
      condition.Awake(player);
      return condition;
    }
    internal Condition GetCondition(Agent agent)
    {
      var condition = CreateCondition();
      condition.Awake(agent);
      return condition;
    }
    protected abstract Condition CreateCondition();
  }

  public abstract class ConditionSO<T> : ConditionSO where T : Condition, new()
  {
    protected override Condition CreateCondition()
    {
      return new T();
    }
  }
}
