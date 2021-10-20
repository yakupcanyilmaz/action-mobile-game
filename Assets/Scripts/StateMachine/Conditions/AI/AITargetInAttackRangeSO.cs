using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AITargetInAttackRange", menuName = "Shooter Prototype/State Machines/Conditions/AI/TargetInAttackRange")]
public class AITargetInAttackRangeSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new AITargetInAttackRange();
  }
}

public class AITargetInAttackRange : Condition
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
    statement = TargetInAttackRange();
  }

  // Func<bool> TargetInAttackRange() => () => (Vector3.SqrMagnitude(this.agent.Targets[0].position - this.agent.transform.position) < this.agent.AttackRadius * this.agent.AttackRadius) && this.agent.CurrentHealth > 0f;
  Func<bool> TargetInAttackRange() => () => (this.agent.SqrMagnitudeDistanceToTarget() != 0f && this.agent.SqrMagnitudeDistanceToTarget() < this.agent.AttackRadius * this.agent.AttackRadius) && this.agent.Health.CurrentHealth > 0f && Time.time > this.agent.AttackTime;
}




