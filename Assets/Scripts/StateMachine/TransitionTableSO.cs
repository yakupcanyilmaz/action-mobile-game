using System.Collections.Generic;
using UnityEngine;

namespace ShooterPrototype.StateMachines.ScriptableObjects
{
  [CreateAssetMenu(fileName = "TransitionTable", menuName = "Shooter Prototype/State Machines/TransitionTable")]
  public class TransitionTableSO : ScriptableObject
  {
    public List<TransitionItem> TransitionItems;
    internal List<Transition> GetTransitions(Player player)
    {
      var transitions = new List<Transition>();
      for (int i = 0; i < TransitionItems.Count; i++)
      {
        transitions.Add(TransitionItems[i].GetTransition(player));
      }
      return transitions;
    }
    internal List<Transition> GetTransitions(Agent agent)
    {
      var transitions = new List<Transition>();
      for (int i = 0; i < TransitionItems.Count; i++)
      {
        transitions.Add(TransitionItems[i].GetTransition(agent));
      }
      return transitions;
    }
  }
}
