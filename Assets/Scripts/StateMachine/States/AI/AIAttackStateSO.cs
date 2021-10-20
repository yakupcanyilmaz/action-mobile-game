using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIAttackState", menuName = "Shooter Prototype/State Machines/States/AI/AttackState")]
public class AIAttackStateSO : StateSO
{
  protected override State CreateState()
  {
    return new AIAttackState();
  }
}

public class AIAttackState : State
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
  }
  public override void OnEnter()
  {
    agent.Animator.SetTrigger(agent.AnimationAttackID);
    agent.Animator.ResetTrigger(agent.AnimationStopAttackID);
  }
  public override void Tick()
  {
    agent.transform.forward = this.agent.Target.transform.position - agent.transform.position;
  }
  public override void OnExit()
  {
    agent.Animator.ResetTrigger(agent.AnimationAttackID);
    agent.Animator.SetTrigger(agent.AnimationStopAttackID);
  }
}


