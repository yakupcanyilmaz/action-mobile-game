using UnityEngine;

public class Soul : MonoBehaviour
{
  [Header("Soul Settings")]
  [SerializeField] protected Bar soulBar;
  [SerializeField] protected float maxSoul = 100f;
  [SerializeField] private GameEventSO OnCharmed;
  
  [Header("Mesh Settings")]
  [SerializeField] private SkinnedMeshRenderer smr;
  [SerializeField] private SkinnedMeshRenderer smrEnemy;
  [SerializeField] private SkinnedMeshRenderer smrPlayer;

  internal bool isCharmed;
  internal float CurrentSoul { get { return currentSoul; } }
  protected float currentSoul;
  public event System.Action<float> OnSoulPctChanged;

  private void OnEnable()
  {
    OnSoulPctChanged += HandleSoulChanged;
    currentSoul = maxSoul;
    soulBar.UpdateBar(1f);
    isCharmed = false;
    if (smr == null) smr = GetComponent<SkinnedMeshRenderer>();
    smr.sharedMesh = smrEnemy.sharedMesh;
  }

  private void OnDisable()
  {
    OnSoulPctChanged -= HandleSoulChanged;
  }

  public void TakeDamageToSoul(float damage)
  {
    if (currentSoul > 0f)
    {
      currentSoul -= damage;
      currentSoul = Mathf.Clamp(currentSoul, 0f, maxSoul);
      float currentSoulPct = (float)currentSoul / (float)maxSoul;
      OnSoulPctChanged(currentSoulPct);
    }
    if (currentSoul <= 0 && !isCharmed)
    {
      Agent agent = GetComponent<Agent>();
      if (agent.Side == Side.Enemy)
      {
        agent.Side = Side.Player;
        smr.sharedMesh = smrPlayer.sharedMesh;
        Charmed();
      }
    }
  }

  private void HandleSoulChanged(float pct)
  {
    soulBar.UpdateBar(pct);
  }

  public void Charmed()
  {
    isCharmed = true;
    if (OnCharmed != null) OnCharmed.Invoke();
    GetComponent<BoxCollider>().enabled = false;
  }
}
