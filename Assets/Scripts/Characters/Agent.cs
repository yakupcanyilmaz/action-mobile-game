using UnityEngine;
using UnityEngine.AI;
using ShooterPrototype.StateMachines;

public class Agent : Character
{
  [Header("Animation Settings")]
  [SerializeField] private Animator animator;
  public Animator Animator { get { return animator; } set { animator = value; } }
  [SerializeField] private float attackDelay = 0.1f;
  public float AttackDelay { get { return attackDelay; }}
  private float attackTime;
  public float AttackTime { get { return attackTime; } set { attackTime = value; } }

  [Header("Events Settings")]
  public GameEventSO OnAISpawned;

  private static readonly int animationMovementID = Animator.StringToHash("Movement");
  private static readonly int animationAttackID = Animator.StringToHash("Attack");
  private static readonly int animationStopAttackID = Animator.StringToHash("StopAttack");
  private static readonly int animationDieID = Animator.StringToHash("Die");
  private static readonly int animationWinID = Animator.StringToHash("Win");
  public int AnimationMovementID { get { return animationMovementID; } }
  public int AnimationAttackID { get { return animationAttackID; } }
  public int AnimationStopAttackID { get { return animationStopAttackID; } }
  public int AnimationDieID { get { return animationDieID; } }
  public int AnimationWinID { get { return animationWinID; } }
  private NavMeshAgent navMeshAgent;
  public float AttackRadius { get { return navMeshAgent.stoppingDistance; } }
  public NavMeshAgent NavMeshAgent { get { return navMeshAgent; } set { navMeshAgent = value; } }

  private Soul soul;
  public Soul Soul { get { return soul; } }

  internal bool isWin;

  protected override void OnEnable()
  {
    base.OnEnable();

    if (navMeshAgent == null) navMeshAgent = GetComponent<NavMeshAgent>();
    if (animator == null) animator = GetComponent<Animator>();

    StateMachine.anyState = anyState.GetState(this);
    StateMachine.idleState = idleState.GetState(this);

    var transitionsFromTable = StateMachine.transitionTable.GetTransitions(this);
    for (int i = 0; i < transitionsFromTable.Count; i++)
    {
      if (transitionsFromTable[i].From.GetType() != StateMachine.anyState.GetType())
      {
        StateMachine.AddTransition(transitionsFromTable[i].From, transitionsFromTable[i].To, transitionsFromTable[i].Condition);
      }
      else if (transitionsFromTable[i].From.GetType() == StateMachine.anyState.GetType())
      {
        StateMachine.AddAnyTransition(transitionsFromTable[i].To, transitionsFromTable[i].Condition);
      }
    }

    StateMachine.SetState(StateMachine.idleState);

    side = Side.Enemy;
    soul = GetComponent<Soul>();
    GetComponent<BoxCollider>().enabled = true;
    isWin = false;
  }

  public void Win()
  {
    isWin = true;
  }

  private void Update()
  {
    if (targets.Count > 0)
    {
      SearchForTargets();
    }
    StateMachine.Tick();
  }

  void DieEvent()
  {
    Poolable.TryPool(gameObject);
  }

  void WinEvent()
  {
    Poolable.TryPool(gameObject);
  }
}