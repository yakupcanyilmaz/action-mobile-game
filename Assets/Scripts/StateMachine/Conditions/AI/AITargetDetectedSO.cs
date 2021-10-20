using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AITargetDetected", menuName = "Shooter Prototype/State Machines/Conditions/AI/TargetDetected")]
public class AITargetDetectedSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new AITargetDetected();
  }
}

public class AITargetDetected : Condition
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
    statement = TargetDetected();
  }

  Func<bool> TargetDetected() => () => (this.agent.Targets.Count > 0 && this.agent.Health.CurrentHealth > 0f);
}




