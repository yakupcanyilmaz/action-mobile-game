using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
  [SerializeField] private string soundID = "RedArrowImpact";
  private AudioSource source;

  private void OnEnable()
  {
    source = GetComponent<AudioSource>();
    AudioClip clip = SoundLibrary.Instance.GetClipFromName(soundID);
    source.pitch = Random.Range(0.8f, 1.2f);
    source.PlayOneShot(clip);
  }


}
