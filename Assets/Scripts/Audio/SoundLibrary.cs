using UnityEngine;
using System.Collections.Generic;

public class SoundLibrary : Singleton<SoundLibrary>
{

  [SerializeField] private Clip[] clips;

  Dictionary<string, AudioClip> groupDictionary = new Dictionary<string, AudioClip>();

  protected override void Awake()
  {
    base.Awake();
    foreach (Clip clip in clips)
    {
      groupDictionary.Add(clip.groupID, clip.clip);
    }
  }

  public AudioClip GetClipFromName(string name)
  {
    if (groupDictionary.ContainsKey(name))
    {
      AudioClip sound = groupDictionary[name];
      return sound;
    }
    return null;
  }

  [System.Serializable]
  public class Clip
  {
    public string groupID;
    public AudioClip clip;
  }
}
