using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIHasNoHealth", menuName = "Shooter Prototype/State Machines/Conditions/AI/HasNoHealth")]
public class AIHasNoHealthSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new AIHasNoHealth();
  }
}

public class AIHasNoHealth : Condition
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
    statement = HasNoHealth();
  }

  Func<bool> HasNoHealth() => () => this.agent.Health.CurrentHealth <= 0f;
}




