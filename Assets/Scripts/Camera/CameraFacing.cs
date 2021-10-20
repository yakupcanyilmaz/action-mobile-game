using UnityEngine;

public class CameraFacing : MonoBehaviour
{
  private Transform cam;

  private void Start()
  {
    cam = Camera.main.transform;
  }

  private void LateUpdate()
  {
    Vector3 direction = cam.transform.forward;
    transform.forward = -direction;
  }
}
