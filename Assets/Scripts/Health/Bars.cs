using UnityEngine;

public class Bars : MonoBehaviour
{
  public Bar healthBar;
  public Bar manaBar;

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

  public void UpdateHealthBar(float normalizedValue)
  {
    Vector3 scale = Vector3.one;

    if (healthBar.foregroundBar != null)
    {
      scale.x = normalizedValue;
      healthBar.foregroundBar.transform.localScale = scale;
    }

    if (healthBar.backgroundBar != null)
    {
      scale.x = 1 - normalizedValue;
      healthBar.backgroundBar.transform.localScale = scale;
    }
  }
  public void UpdateManaBar(float normalizedValue)
  {
    Vector3 scale = Vector3.one;

    if (manaBar.foregroundBar != null)
    {
      scale.x = normalizedValue;
      manaBar.foregroundBar.transform.localScale = scale;
    }

    if (manaBar.backgroundBar != null)
    {
      scale.x = 1 - normalizedValue;
      manaBar.backgroundBar.transform.localScale = scale;
    }
  }


}

// [Serializable]
// public class Bar
// {
//   public Transform foregroundBar;

//   public Transform backgroundBar;
// }
