using System;
using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerHasNoHealth", menuName = "Shooter Prototype/State Machines/Conditions/Player/HasNoHealth")]
public class PlayerHasNoHealthConditionSO : ConditionSO
{
  protected override Condition CreateCondition()
  {
    return new PlayerHasNoHealthCondition();
  }
}

public class PlayerHasNoHealthCondition : Condition
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
    statement = HasNoHealth();
  }

  Func<bool> HasNoHealth() => () => this.player.Health.CurrentHealth <= 0f;
}




