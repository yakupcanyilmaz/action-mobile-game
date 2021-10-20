using UnityEngine;

public class Warrior : Agent
{
  [Header("Attack Settings")]
  [SerializeField] private WeaponSO sword;
  [SerializeField] private Transform attackPoint;

  void HitEvent()
  {
    GetComponent<Character>().Target.Health.TakeDamageToHealth(sword.damage);
    sword.PlayParticles(sword.weapon.impactParticles, attackPoint);
  }
}
