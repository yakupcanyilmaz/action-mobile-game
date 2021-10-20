using System;
using System.Collections.Generic;
using ShooterPrototype.StateMachines.ScriptableObjects;

namespace ShooterPrototype.StateMachines
{
  public class StateMachine
  {
    public TransitionTableSO transitionTable;
    public State idleState;
    public State anyState;

    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> currentTransitions = new List<Transition>();
    private List<Transition> anyTransitions = new List<Transition>();
    private static List<Transition> emptyTransitions = new List<Transition>(0);
    private State currentState;

    public StateMachine(TransitionTableSO transitionTable)
    {
      this.transitionTable = transitionTable;
    }

    public void Tick()
    {
      var transition = GetTransition();
      if (transition != null)
      {
        SetState(transition.To);
      }
      currentState?.Tick();
    }

    public void SetState(State state)
    {
      if (state == currentState) return;

      currentState?.OnExit();
      currentState = state;

      transitions.TryGetValue(currentState.GetType(), out currentTransitions);
      if (currentTransitions == null)
      {
        currentTransitions = emptyTransitions;
      }

      currentState.OnEnter();
    }

    public void AddTransitionFromTable(State from, State to, Condition condition)
    {
      AddTransition(from, to, condition);
    }

    public void AddTransition(State from, State to, Condition condition)
    {
      if (transitions.TryGetValue(from.GetType(), out var transitionList) == false)
      {
        transitionList = new List<Transition>();
        transitions[from.GetType()] = transitionList;
      }
      transitionList.Add(new Transition(from, to, condition));
    }

    public void AddAnyTransition(State to, Condition condition)
    {
      anyTransitions.Add(new Transition(null, to, condition));
    }

    private Transition GetTransition()
    {
      for (int i = 0; i < currentTransitions.Count; i++)
      {
        if (currentTransitions[i].Condition.Statement())
        {
          return currentTransitions[i];
        }
      }
      for (int i = 0; i < anyTransitions.Count; i++)
      {
        if (anyTransitions[i].Condition.Statement())
        {
          return anyTransitions[i];
        }
      }
      return null;
    }

  }
}

