using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIIdleState", menuName = "Shooter Prototype/State Machines/States/AI/IdleState")]
public class AIIdleStateSO : StateSO
{
  protected override State CreateState()
  {
    return new AIIdleState();
  }
}

public class AIIdleState : State
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
  }
  public override void OnEnter()
  {
    agent.Animator.SetFloat(agent.AnimationMovementID, 0f);
  }
  public override void Tick()
  {

  }
  public override void OnExit()
  {
    agent.AttackTime = Time.time + agent.AttackDelay;
  }
}

