using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerRunState", menuName = "Shooter Prototype/State Machines/States/Player/RunState")]
public class PlayerRunStateSO : StateSO
{
  protected override State CreateState()
  {
    return new PlayerRunState();
  }
}

public class PlayerRunState : State
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
  }
  public override void OnEnter()
  {
    player.Animator.SetFloat(player.AnimationMovementID, 1f);
  }
  public override void Tick()
  {

  }
  public override void OnExit()
  {
    player.Animator.SetFloat(player.AnimationMovementID, 0f);
  }

}



