using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIRunState", menuName = "Shooter Prototype/State Machines/States/AI/RunState")]
public class AIRunStateSO : StateSO
{
  protected override State CreateState()
  {
    return new AIRunState();
  }
}
public class AIRunState : State
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
  }
  public override void OnEnter()
  {
    agent.NavMeshAgent.isStopped = false;
    agent.Animator.SetFloat(agent.AnimationMovementID, 1f);
  }
  public override void Tick()
  {
    agent.NavMeshAgent.SetDestination(agent.Target.transform.position);
  }
  public override void OnExit()
  {
    agent.AttackTime = Time.time + agent.AttackDelay;
    agent.Animator.SetFloat(agent.AnimationMovementID, 0f);
    agent.NavMeshAgent.isStopped = true;
  }
}


