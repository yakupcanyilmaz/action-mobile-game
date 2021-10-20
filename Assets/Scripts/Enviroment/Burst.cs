using UnityEngine;

public class Burst : MonoBehaviour
{
  [SerializeField] private float radius = 2f;
  [SerializeField] private float damage = 20f;
  [SerializeField] private AudioSource source;
  [SerializeField] private Mode mode;
  public Mode Mode {get {return mode;}}
  [SerializeField] private string buffSoundID = "BuffRed";

  void BurstEvent()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
    foreach (Collider nearbyObject in colliders)
    {
      if(mode == Mode.Red)
      {
        var character = nearbyObject.GetComponent<Character>();
        if (character != null)
        {
          character.Health.TakeDamageToHealth(damage);
        }
      }
      if(mode == Mode.Blue)
      {
        var agent = nearbyObject.GetComponent<Agent>();
        if (agent != null)
        {
          agent.Soul.TakeDamageToSoul(damage);
        }
      }
    }
    AudioClip clip = SoundLibrary.Instance.GetClipFromName(buffSoundID);
    source.PlayOneShot(clip);
  }
  void BurstEndEvent()
  {
    gameObject.SetActive(false);
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(transform.position, radius);
  }

}
