using System;

namespace ShooterPrototype.StateMachines.ScriptableObjects
{
  [Serializable]
  public class TransitionItem
  {
    internal Transition GetTransition(Player player)
    {
      var fromState = From.GetState(player);
      var toState = To.GetState(player);
      var condition = Condition.GetCondition(player);
      return new Transition(fromState, toState, condition);
    }
    internal Transition GetTransition(Agent agent)
    {
      var fromState = From.GetState(agent);
      var toState = To.GetState(agent);
      var condition = Condition.GetCondition(agent);
      return new Transition(fromState, toState, condition);
    }

    public StateSO From;
    public StateSO To;
    public ConditionSO Condition;
  }
}
