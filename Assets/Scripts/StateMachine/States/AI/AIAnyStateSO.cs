using UnityEngine;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

[CreateAssetMenu(fileName = "AIAnyState", menuName = "Shooter Prototype/State Machines/States/AI/AnyState")]
public class AIAnyStateSO : StateSO
{
  protected override State CreateState()
  {
    return new AIAnyState();
  }
  // public override void OnEnter()
  // {

  // }
  // public override void Tick()
  // {

  // }
  // public override void OnExit()
  // {

  // }
}

public class AIAnyState : State
{
  private Agent agent;

  public override void Awake(Agent agent)
  {
    this.agent = agent;
  }
}

