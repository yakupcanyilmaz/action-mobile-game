using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterPrototype.StateMachines
{
  public abstract class State
  {
    public virtual void Awake(Player player) { }
    public virtual void Awake(Agent agent) { }
    public virtual void OnEnter() { }
    public virtual void Tick() { }
    public virtual void OnExit() { }
  }
}
