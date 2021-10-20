using UnityEngine;
public class Damager : MonoBehaviour
{
  [SerializeField] public float damage = 10f;
  [SerializeField] public Side side;
  // [SerializeField] bool isProjectile;
  [SerializeField] public ParticleSystem impactParticles;

  protected virtual void OnTriggerEnter(Collider other)
  {
    var character = other.GetComponent<Character>();
    if (character != null)
    {
      if (character.Side != this.side)
      {
        character.Health.TakeDamageToHealth(damage);
        if (impactParticles != null)
        {
          var pfx = Poolable.TryGetPoolable<ParticleSystem>(impactParticles.gameObject);
          pfx.transform.position = transform.position;
          pfx.Play();
        }
      }
    }
  }
}
