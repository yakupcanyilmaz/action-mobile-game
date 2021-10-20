using System;

namespace ShooterPrototype.StateMachines
{
  public abstract class Condition
  {
    public virtual void Awake(Player player) { }
    public virtual void Awake(Agent agent) { }
    protected Func<bool> statement;
    internal Func<bool> Statement { get { return statement; } }
  }
}

