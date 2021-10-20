using UnityEngine;

public class MainMenu : SimpleMenu<MainMenu>
{
  [SerializeField] private GameObject mobileControls;
  [SerializeField] private GameObject desktopControls;

  private void Start()
  {
    MenuManager.Instance.mobileJoystick.SetActive(false);
    if (GameManager.Instance.isMobileGame)
    {
      mobileControls.SetActive(true);
      desktopControls.SetActive(false);
    }
    else
    {
      desktopControls.SetActive(true);
      mobileControls.SetActive(false);
    }
  }

  public void OnPlayPressed()
  {
    GameMenu.Show();
    GameManager.Instance.StartGame();
    GameManager.Instance.TogglePauseState();
  }

  public void OnOptionsPressed()
  {
    OptionsMenu.Show();
  }

  public override void OnBackPressed()
  {
    Application.Quit();
  }
}
