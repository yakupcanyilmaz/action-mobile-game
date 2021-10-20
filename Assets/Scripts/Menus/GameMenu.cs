using UnityEngine;

public class GameMenu : SimpleMenu<GameMenu>
{
  public GameObject blackScreen;
  [SerializeField] private GameObject mobileControls;

  private void Start()
  {
    if (GameManager.Instance.isMobileGame)
    {
      mobileControls.SetActive(true);
      MenuManager.Instance.mobileJoystick.SetActive(true);
    }
    else
    {
      mobileControls.SetActive(false);
      MenuManager.Instance.mobileJoystick.SetActive(false);
    }
  }

  public override void OnBackPressed()
  {
    PauseMenu.Show();
  }

  public void OnPauseButtonPressed()
  {
    PauseMenu.Show();
    GameManager.Instance.TogglePauseState();
  }

}
