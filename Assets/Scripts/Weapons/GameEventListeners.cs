using UnityEngine;

public class GameEventListeners : MonoBehaviour
{
  [SerializeField] private GameEventListener[] gameEventListeners;
  private void OnEnable()
  {
    for (int i = 0; i < gameEventListeners.Length; i++)
    {
      gameEventListeners[i].gameEvent.Register(gameEventListeners[i]);
    }
  }
  private void OnDisable()
  {
    for (int i = 0; i < gameEventListeners.Length; i++)
    {
      gameEventListeners[i].gameEvent.Deregister(gameEventListeners[i]);
    }
  }

}
