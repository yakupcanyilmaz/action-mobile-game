using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "Shooter Prototype/State Machines/States/Player/IdleState")]
public class PlayerIdleStateSO : StateSO
{
  protected override State CreateState()
  {
    return new PlayerIdleState();
  }
}

public class PlayerIdleState : State
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
  }
  public override void OnEnter()
  {
    player.Animator.SetFloat(player.AnimationMovementID, 0f);
  }
  public override void Tick()
  {

  }
  public override void OnExit()
  {

  }
}

