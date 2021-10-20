using UnityEngine;
using UnityEngine.UI;

public class GameEndMenu : SimpleMenu<GameEndMenu>
{
  [SerializeField] private Text youWonText;
  [SerializeField] private Text youLostText;

  public void OnQuitPressed()
  {
    Hide();
    Destroy(this.gameObject); // This menu does not automatically destroy itself

    GameMenu.Hide();
    MenuManager.Instance.LoadMainMenu();
  }

  public void OpenWinMenu()
  {
    GameManager.Instance.TogglePauseState();
    AudioManager.Instance.PlaySound("WinSound");
    youWonText.gameObject.SetActive(true);
    youLostText.gameObject.SetActive(false);
  }

  public void OpenLoseMenu()
  {
    GameManager.Instance.TogglePauseState();
    AudioManager.Instance.PlaySound("LoseSound");
    youWonText.gameObject.SetActive(false);
    youLostText.gameObject.SetActive(true);
  }
}