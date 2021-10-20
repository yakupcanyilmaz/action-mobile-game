using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField] protected Bar healthBar;
  [SerializeField] protected Float health;

  internal bool isDead;
  internal float CurrentHealth { get { return currentHealth; } }
  protected float currentHealth;
  public event System.Action<float> OnHealthPctChanged;
  [SerializeField] private GameEventSO OnDeath;

  private void Start() 
  {
    healthBar.UpdateBar(1f);
    health.Value = health.ConstantValue;
    currentHealth = health.ConstantValue;
  }

  private void OnEnable()
  {
    OnHealthPctChanged += HandleHealthChanged;
    if (health.UseConstant)
    {
      healthBar.UpdateBar(1f);
    }
    currentHealth = health.Value;
    isDead = false;
  }

  private void OnDisable()
  {
    OnHealthPctChanged -= HandleHealthChanged;
  }

  public void TakeDamageToHealth(float damage)
  {
    if (currentHealth > 0f)
    {
      currentHealth -= damage;
      currentHealth = Mathf.Clamp(currentHealth, 0f, health.ConstantValue);
      health.Value = currentHealth;
      float currentHealthPct = (float)currentHealth / (float)health.ConstantValue;
      OnHealthPctChanged(currentHealthPct);
    }
    if (currentHealth <= 0 && !isDead)
    {
      Die();
    }
  }

  private void HandleHealthChanged(float pct)
  {
    healthBar.UpdateBar(pct);
  }

  public void Die()
  {
    isDead = true;
    if (OnDeath != null) OnDeath.Invoke();
    GetComponent<Collider>().enabled = false;
  }
}
