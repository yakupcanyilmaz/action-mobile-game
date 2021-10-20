using UnityEngine;

public class TargetFollow : MonoBehaviour
{
  [SerializeField] private Transform target;
  private Vector3 offset;

  void Start()
  {
    offset = transform.position - target.position;
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = target.position + offset;
  }
}
