using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AITargetOutOfAttackRange", menuName = "Shooter Prototype/State Machines/Conditions/AI/TargetOutOfAttackRange")]
public class AITargetOutOfAttackRangeSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new AITargetOutOfAttackRange();
  }
}

public class AITargetOutOfAttackRange : Condition
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
    statement = TargetOutOfAttackRange();
  }

  // Func<bool> TargetOutOfAttackRange() => () => (Vector3.SqrMagnitude(this.agent.Targets[0].position - this.agent.transform.position) > this.agent.AttackRadius * this.agent.AttackRadius && this.agent.CurrentHealth > 0f);
  Func<bool> TargetOutOfAttackRange() => () => (this.agent.SqrMagnitudeDistanceToTarget() != 0f && this.agent.SqrMagnitudeDistanceToTarget() > this.agent.AttackRadius * this.agent.AttackRadius) && this.agent.Health.CurrentHealth > 0f;
}





