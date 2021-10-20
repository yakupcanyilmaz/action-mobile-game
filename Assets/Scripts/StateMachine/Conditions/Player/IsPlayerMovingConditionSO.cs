using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPlayerMoving", menuName = "Shooter Prototype/State Machines/Conditions/Player/IsMoving")]
public class IsPlayerMovingConditionSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new IsPlayerMovingCondition();
  }
}

public class IsPlayerMovingCondition : Condition
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
    statement = IsPlayerMove();
  }

  Func<bool> IsPlayerMove() => () => this.player.MovementCurrentInput == true;
}




