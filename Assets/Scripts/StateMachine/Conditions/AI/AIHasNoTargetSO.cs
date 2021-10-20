using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIHasNoTarget", menuName = "Shooter Prototype/State Machines/Conditions/AI/HasNoTarget")]
public class AIHasNoTargetSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new AIHasNoTarget();
  }
}

public class AIHasNoTarget : Condition
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
    statement = HasNoTarget();
  }

  Func<bool> HasNoTarget() => () => ((this.agent.Targets == null || this.agent.Targets.Count == 0) && this.agent.Health.CurrentHealth > 0f);
}




