using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPlayerStopped", menuName = "Shooter Prototype/State Machines/Conditions/Player/IsStopped")]
public class IsPlayerStoppedConditionSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new IsPlayerStoppedCondition();
  }
}

public class IsPlayerStoppedCondition : Condition
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
    statement = IsPlayerNotMove();
  }

  Func<bool> IsPlayerNotMove() => () => this.player.MovementCurrentInput == false && this.player.Targets.Count == 0;
}




