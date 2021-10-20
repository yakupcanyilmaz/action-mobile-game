using System;

[Serializable]
public class Float
{
  public bool UseConstant = true;
  public float ConstantValue;
  public FloatSO Variable;

  public float Value
  {
    get
    {
      return UseConstant ? ConstantValue : Variable.Value;
    }
    set
    {
      if(!UseConstant)
      {
        Variable.Value = value;
      }
    }
  }
}
