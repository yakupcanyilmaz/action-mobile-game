using UnityEngine;

public class Bar : MonoBehaviour
{
  public Transform foregroundBar;

  public Transform backgroundBar;

  public void UpdateBar(float normalizedValue)
  {
    Vector3 scale = Vector3.one;

    if (foregroundBar != null)
    {
      scale.x = normalizedValue;
      foregroundBar.transform.localScale = scale;
    }

    if (backgroundBar != null)
    {
      scale.x = 1 - normalizedValue;
      backgroundBar.transform.localScale = scale;
    }
  }
}

