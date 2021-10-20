using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Shooter Prototype/Weapons/NewWeapon")]
public class WeaponSO : ScriptableObject
{
  [SerializeField] public float damage = 0f;
  [SerializeField] public Weapon weapon = null;
  [SerializeField] public Projectile projectile = null;
  
  public void LaunchProjectile(Transform attackPoint, Quaternion rotation, Character target)
  {
    var projectile = Poolable.TryGetPoolable<Projectile>(this.projectile.gameObject);
    if (projectile == null) return;
    projectile.SetDamage(damage, target);
    projectile.transform.position = attackPoint.position;
    projectile.transform.rotation = rotation;
    PlayParticles(projectile.flashParticles, attackPoint);
  }

  public void PlayParticles(ParticleSystem particles, Transform point)
  {
    if(particles == null) return;
    var pfx = Poolable.TryGetPoolable<ParticleSystem>(particles.gameObject);
    pfx.transform.SetParent(point, false);
    pfx.Play();
  }
}



