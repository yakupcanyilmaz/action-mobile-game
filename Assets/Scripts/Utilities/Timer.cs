using System;

public class Timer
{
  readonly Action callback;
  float time, currentTime;

  public Timer(float newTime, Action onElapsed = null)
  {
    SetTime(newTime);

    currentTime = 0f;
    callback += onElapsed;
  }

  public virtual bool Tick(float deltaTime)
  {
    return AssessTime(deltaTime);
  }

  protected bool AssessTime(float deltaTime)
  {
    currentTime += deltaTime;
    if (currentTime >= time)
    {
      FireEvent();
      return true;
    }

    return false;
  }
  public void FireEvent()
  {
    callback.Invoke();
  }

  public void Reset()
  {
    currentTime = 0;
  }

  public void SetTime(float newTime)
  {
    time = newTime;

    if (newTime <= 0)
    {
      time = 0.1f;
    }
  }
}
