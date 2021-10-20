using UnityEngine;

public class CameraController : Singleton<CameraController>
{
  [SerializeField] private Transform target;
  [SerializeField] private Vector3 offset;
  private Vector3 moveVelocity;

  private void FixedUpdate()
  {
    Vector3 trg = target.position + offset;
    transform.position = Vector3.SmoothDamp(transform.position, trg, ref moveVelocity, 0.2f);
  }
}
