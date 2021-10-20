namespace ShooterPrototype.StateMachines
{
  public class Transition
  {
    public Transition(State from, State to, Condition condition)
    {
      From = from;
      To = to;
      Condition = condition;
    }
    public State From;
    public State To;
    public Condition Condition;
  }
}
