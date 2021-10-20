using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIWin", menuName = "Shooter Prototype/State Machines/Conditions/AI/Win")]
public class AIWinSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new AIWin();
  }
}

public class AIWin : Condition
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
    statement = Win();
  }

  Func<bool> Win() => () => this.agent.Health.CurrentHealth > 0f && this.agent.isWin;
}




