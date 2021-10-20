using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerAnyState", menuName = "Shooter Prototype/State Machines/States/Player/AnyState")]
public class PlayerAnyStateSO : StateSO
{
  protected override State CreateState()
  {
    return new PlayerAnyState();
  }
}

public class PlayerAnyState : State
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
  }
}

