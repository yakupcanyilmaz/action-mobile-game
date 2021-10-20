using UnityEngine;

public class Mage : Agent
{
  [Header("Attack Settings")]
  [SerializeField] private WeaponSO spellRed;
  [SerializeField] private Transform attackPoint;
  private WeaponSO currentSpell;

  protected override void OnEnable()
  {
    base.OnEnable();
    currentSpell = spellRed;
  }

  void FireEvent()
  {
    if (currentSpell == null) return;
    if (currentSpell)
    {
      currentSpell.LaunchProjectile(attackPoint, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), target);
    }
  }
}
