using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerFireState", menuName = "Shooter Prototype/State Machines/States/Player/FireState")]
public class PlayerFireStateSO : StateSO
{
  protected override State CreateState()
  {
    return new PlayerFireState();
  }
}
public class PlayerFireState : State
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
  }
  public override void OnEnter()
  {
    player.Animator.SetTrigger(player.AnimationFireID);
    player.Animator.ResetTrigger(player.AnimationStopFireID);
  }
  public override void Tick()
  {
    player.UpdateFireBehavior();
  }
  public override void OnExit()
  {
    player.Animator.ResetTrigger(player.AnimationFireID);
    player.Animator.SetTrigger(player.AnimationStopFireID);
  }
}

