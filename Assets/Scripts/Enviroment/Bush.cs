using UnityEngine;

public class Bush : MonoBehaviour
{
  [SerializeField] private Burst burst;
  [SerializeField] private AudioSource source;
  [SerializeField] private string swooshSoundID = "SwooshRed";

  private void OnEnable()
  {
    burst.gameObject.SetActive(false);
  }

  private void OnTriggerEnter(Collider other)
  {
    Player player = other.GetComponent<Player>();
    if (player && player.AttackMode == burst.Mode)
    {
      burst.gameObject.SetActive(true);
      AudioClip clip = SoundLibrary.Instance.GetClipFromName(swooshSoundID);
      source.PlayOneShot(clip);
    }
  }
}
