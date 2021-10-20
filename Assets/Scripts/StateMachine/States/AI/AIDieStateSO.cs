using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIDieState", menuName = "Shooter Prototype/State Machines/States/AI/DieState")]
public class AIDieStateSO : StateSO
{
  protected override State CreateState()
  {
    return new AIDieState();
  }
}
public class AIDieState : State
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
  }
  public override void OnEnter()
  {
    agent.Animator.SetTrigger(agent.AnimationDieID);
  }
  public override void Tick()
  {

  }
  public override void OnExit()
  {

  }
}


