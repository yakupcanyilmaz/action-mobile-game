using UnityEngine;
using System.Collections.Generic;
using ShooterPrototype.StateMachines;
using ShooterPrototype.StateMachines.ScriptableObjects;

public class Character : MonoBehaviour
{
  [Header("State Machine Settings")]
  [SerializeField] protected TransitionTableSO transitionTable;
  [SerializeField] protected StateSO idleState;
  [SerializeField] protected StateSO anyState;
  private StateMachine stateMachine;
  protected StateMachine StateMachine
  {
    get
    {
      if (stateMachine != null)
      {
        return stateMachine;
      }
      stateMachine = new StateMachine(transitionTable);
      return stateMachine;
    }
  }

  protected Health health;
  public Health Health { get { return health; }}

  [SerializeField] protected Side side;
  public Side Side { get { return side; }  set { side = value; } }

  protected List<Character> targets = new List<Character>();
  public List<Character> Targets { get { return targets; } }

  protected Character target;
  public Character Target { get { return target; } }

  
  protected Player player;
  public Player Player { get { return player; } }

  protected virtual void OnEnable()
  {
    player = GameObject.FindObjectOfType<Player>();
    health = GetComponent<Health>();
  }

  public void UpdateTargetList()
  {
    ClearTargetList();
    if (side == Side.Enemy)
    {
      if (!player.Health.isDead) this.targets.Add(player);
    }
    if (side == Side.Player)
    {
      if (!player.Health.isDead)
      {
        var allTargets = GameObject.FindObjectsOfType<Agent>();
        foreach (Agent target in allTargets)
        {
          if (!target.Health.isDead && target.Side != this.side) this.targets.Add(target);
        }
      }
    }
  }

  protected void ClearTargetList()
  {
    targets.Clear();
  }

  protected void SearchForTargets()
  {
    targets.Sort(ByDistance);
    target = targets[0];
  }

  private int ByDistance(Character first, Character second)
  {
    float firstSqrMagnitude = Vector3.SqrMagnitude(first.transform.position - transform.position);
    float secondSqrMagnitude = Vector3.SqrMagnitude(second.transform.position - transform.position);
    return firstSqrMagnitude.CompareTo(secondSqrMagnitude);
  }

  public float SqrMagnitudeDistanceToTarget()
  {
    if (Targets.Count > 0)
    {
      return Vector3.SqrMagnitude(target.transform.position - transform.position);
    }
    else
    {
      return 0f;
    }
  }
}

public enum Side
{
  Player,
  Enemy,
}
