using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerDieState", menuName = "Shooter Prototype/State Machines/States/Player/DieState")]
public class PlayerDieStateSO : StateSO
{
  protected override State CreateState()
  {
    return new PlayerDieState();
  }
}
public class PlayerDieState : State
{
  private Player player;

  public override void Awake(Player player)
  {
    this.player = player;
  }
  public override void OnEnter()
  {
    player.Animator.SetTrigger(player.AnimationDieID);
  }
  public override void Tick()
  {

  }
  public override void OnExit()
  {

  }
}


