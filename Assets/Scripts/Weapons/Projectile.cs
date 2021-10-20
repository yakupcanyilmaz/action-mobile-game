using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private float speed = 5f;
  [SerializeField] private float lifetime = 5f;
  [SerializeField] public Mode damageMode;
  [SerializeField] public ParticleSystem flashParticles;
  [SerializeField] public ParticleSystem impactParticles;

  private float lifetimer = 0;
  private float damage = 0;
  private Character target;

  private void Update()
  {
    transform.position += transform.forward * speed * Time.deltaTime;

    lifetimer += Time.deltaTime;
    if (lifetimer >= lifetime)
    {
      lifetimer = 0;
      Destroy();
    }
  }

  public void SetDamage(float damage, Character target)
  {
    this.damage = damage;
    this.target = target;
  }

  protected void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<Character>() != target) return;
    if (target != null)
    {
      switch (damageMode)
      {
        case Mode.Red:
          target.GetComponent<Health>().TakeDamageToHealth(damage);
          break;
        case Mode.Blue:
          target.GetComponent<Soul>().TakeDamageToSoul(damage);
          break;
      }
      if (impactParticles != null)
      {
        var pfx = Poolable.TryGetPoolable<ParticleSystem>(impactParticles.gameObject);
        pfx.transform.position = transform.position;
        pfx.Play();
      }
      Destroy();
    }    
  }

  private void Destroy()
  {
    Poolable.TryPool(gameObject);
  }
}
