using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPlayerFiring", menuName = "Shooter Prototype/State Machines/Conditions/Player/IsFiring")]
public class IsPlayerFiringConditionSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new IsPlayerFiringCondition();
  }
}

public class IsPlayerFiringCondition : Condition
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
    statement = IsPlayerInFire();
  }

  Func<bool> IsPlayerInFire() => () => this.player.MovementCurrentInput == false && this.player.Targets.Count > 0;
}




