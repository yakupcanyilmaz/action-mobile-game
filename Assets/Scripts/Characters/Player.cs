using UnityEngine;
using UnityEngine.InputSystem;
using ShooterPrototype.StateMachines;

public class Player : Character
{
  [Header("Control Settings:")]
  public MoveJoystickMobile moveJoystick;
  private float verticalInput;
  private float horizontalInput;

  [Header("Input Settings")]
  [SerializeField] private PlayerInput playerInput;
  [SerializeField] private float movementSmoothingSpeed = 3f;
  private Vector3 rawInputMovement;
  public Vector3 RawInputMovement { set { rawInputMovement = value; } }
  private Vector3 smoothInputMovement;
  private bool movementCurrentInput = false;
  public bool MovementCurrentInput { get { return movementCurrentInput; } }
  private bool isHit = false;

  private static readonly string actionMapPlayerControls = "Player";
  private static readonly string actionMapMenuControls = "UI";

  [Header("Physics Settings")]
  [SerializeField] private CharacterController controller;

  [Header("Movement Settings")]
  [SerializeField] private Float movementSpeed;

  [Header("Animation Settings")]
  [SerializeField] private Animator animator;
  public Animator Animator { get { return animator; } set { animator = value; } }

  private static readonly int animationMovementID = Animator.StringToHash("Movement");
  private static readonly int animationFireID = Animator.StringToHash("Fire");
  private static readonly int animationStopFireID = Animator.StringToHash("StopFire");
  private static readonly int animationHitID = Animator.StringToHash("Hit");
  private static readonly int animationStopHitAndFireID = Animator.StringToHash("StopHitAndFire");
  private static readonly int animationStopHitAndMoveID = Animator.StringToHash("StopHitAndMove");
  private static readonly int animationDieID = Animator.StringToHash("Die");
  public int AnimationMovementID { get { return animationMovementID; } }
  public int AnimationFireID { get { return animationFireID; } }
  public int AnimationStopFireID { get { return animationStopFireID; } }
  public int AnimationDieID { get { return animationDieID; } }

  [Header("Attack Settings")]
  [SerializeField] private WeaponSO bowRed;
  [SerializeField] private WeaponSO bowBlue;
  [SerializeField] private Mode attackMode = Mode.Red;
  public Mode AttackMode { get { return attackMode; } }
  [SerializeField] private float hitRate = 2f;
  [SerializeField] private Transform attackPoint;
  [SerializeField] private SphereCollider hitCollider;
  private WeaponSO currentWeapon;
  private float hitTimer;

  [Header("Audio Settings")]
  [SerializeField] private string soundID = "FootStep";
  [SerializeField] private AudioSource source;

  protected override void OnEnable()
  {
    base.OnEnable();

    if (playerInput == null) playerInput = GetComponent<PlayerInput>();
    if (animator == null) animator = GetComponent<Animator>();
    if (controller == null) controller = GetComponent<CharacterController>();
    if (source == null) source = GetComponent<AudioSource>();

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

    hitTimer = 0f;
    hitCollider.enabled = false;
  }

  private void Start() {
    currentWeapon = bowRed;
  }

  private void Update()
  {
    hitTimer -= Time.deltaTime;

    StateMachine.Tick();

    CalculateMovementInput();
    CalculateMovementInputSmoothing();
    if (targets.Count > 0)
    {
      SearchForTargets();
    }
  }

  private void CalculateMovementInput()
  {
    if (GameManager.Instance.isMobileGame)
    {
      verticalInput = moveJoystick.Vertical;
      horizontalInput = moveJoystick.Horizontal;
      rawInputMovement = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    if (rawInputMovement == Vector3.zero || health.isDead)
    {
      movementCurrentInput = false;
    }
    else if (rawInputMovement != Vector3.zero)
    {
      movementCurrentInput = true;
    }
  }

  private void CalculateMovementInputSmoothing()
  {
    if (movementCurrentInput == true)
    {
      smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
    }
    else if (movementCurrentInput == false)
    {
      smoothInputMovement = Vector3.zero;
    }
  }


  private void FixedUpdate()
  {
    if (isHit == false)
    {
      if (movementCurrentInput == true)
      {
        MovePLayer();
      }
    }
  }

  private void MovePLayer()
  {
    controller.Move(smoothInputMovement * Time.fixedDeltaTime * movementSpeed.Value);
    transform.rotation = Quaternion.LookRotation(smoothInputMovement);
  }

  internal void UpdateFireBehavior()
  {
    if (targets == null) return;
    transform.forward = targets[0].transform.position - transform.position;
  }

  void FireEvent()
  {
    if (movementCurrentInput) return;
    if (currentWeapon == null) return;
    currentWeapon.LaunchProjectile(attackPoint, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), target);
  }

  void DieEvent()
  {
    GameEndMenu.Show();
    GameEndMenu.Instance.OpenLoseMenu();
  }

  public void EnableGameplayControls()
  {
    playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
  }

  public void EnablePauseMenuControls()
  {
    playerInput.SwitchCurrentActionMap(actionMapMenuControls);
  }

  private void OnMove(InputValue value)
  {
    if (!GameManager.Instance.isMobileGame)
    {
      if (health.CurrentHealth > 0f)
      {
        Vector2 inputMovement = value.Get<Vector2>();

        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
      }
    }
  }

  private void OnHit(InputValue value)
  {
    if (hitTimer <= 0f)
    {
      Hit();
    }
  }

  private void OnTogglePause(InputValue value)
  {
    GameManager.Instance.HandleTogglePauseState();
  }

  private void OnSwitchMode(InputValue value)
  {
    switch (attackMode)
    {
      case Mode.Blue:
        attackMode = Mode.Red;
        currentWeapon = bowRed;
        RaycastGameObject.Instance.fieldColor = new Vector4(1, 0, 0, 1);
        break;
      case Mode.Red:
        attackMode = Mode.Blue;
        currentWeapon = bowBlue;
        RaycastGameObject.Instance.fieldColor = new Vector4(0, 0, 1, 1);
        break;
    }
  }

  public void Hit()
  {
    hitTimer = 1 / hitRate;
    animator.SetTrigger(animationHitID);
    animator.ResetTrigger(animationStopHitAndMoveID);
    animator.ResetTrigger(animationStopHitAndFireID);
    isHit = true;
  }

  private void HitEvent()
  {
    hitCollider.enabled = true;
  }

  private void HitEndEvent()
  {
    StopHit();
    hitCollider.enabled = false;
  }

  private void StopHit()
  {
    animator.ResetTrigger(animationHitID);
    if (targets.Count != 0 && !movementCurrentInput)
    {
      animator.SetTrigger(animationStopHitAndFireID);
    }
    else
    {
      animator.SetTrigger(animationStopHitAndMoveID);
    }
    isHit = false;
  }
  private void FootStepEvent()
  {
    AudioClip clip = SoundLibrary.Instance.GetClipFromName(soundID);
    source.pitch = Random.Range(0.8f, 1.2f);
    source.PlayOneShot(clip);
  }
}

public enum Mode
{
  Red,
  Blue,
}
