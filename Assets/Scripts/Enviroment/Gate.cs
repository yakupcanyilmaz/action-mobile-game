using UnityEngine;

public class Gate : MonoBehaviour
{
  // private void OnCollisionEnter(Collision other)
  // {
  //   Debug.Log("hit");
  //   if (other.gameObject.CompareTag("Player"))
  //   {
  //     GameManager.Instance.NextLevel();
  //   }
  // }
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      // if (GameManager.Instance.isLevelCompleted)
      // {
      GameManager.Instance.NextLevel();
      // }
    }
  }
}
