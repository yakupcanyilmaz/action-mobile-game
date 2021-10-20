using UnityEngine;

public class RaycastGameObject : Singleton<RaycastGameObject>
{
  [SerializeField] private float radius, softness;
  public Color fieldColor;
  [SerializeField] private Transform target;

  private void Update()
  {
    Vector4 pos = new Vector4(target.position.x, target.position.y, target.position.z, 0);
    Shader.SetGlobalVector("GLOBALmask_Position", pos);
    Shader.SetGlobalFloat("GLOBALmask_Radius", radius);
    Shader.SetGlobalFloat("GLOBALmask_Softness", softness);
    Shader.SetGlobalVector("GLOBALmask_FieldColor", fieldColor);
  }
}
