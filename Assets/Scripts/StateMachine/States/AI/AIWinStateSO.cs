using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIWinState", menuName = "Shooter Prototype/State Machines/States/AI/WinState")]
public class AIWinStateSO : StateSO
{
  protected override State CreateState()
  {
    return new AIWinState();
  }
}
public class AIWinState : State
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
  }
  public override void OnEnter()
  {
    agent.Animator.SetTrigger(agent.AnimationWinID);
  }
  public override void Tick()
  {

  }
  public override void OnExit()
  {

  }
}


