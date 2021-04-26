
using UnityEngine;

public class Constants : MonoBehaviour
{
  public string RADIO_NEXT_BUTTON_NAME = "NextRadioButton";
  public string RADIO_PAUSE_BUTTON_NAME = "PauseRadioButton";

  public static string CONNECTIONS_PARENT_SELECTOR = "/ConnectionsParent";

  public static string PLAYER_SELECTOR = "/Player";

  public static string TIME_HANDLER_SELECTOR = "/TimeHandler";

  public static string CANVAS_SELECTOR = "/Canvas";
  public static string WIN_ANIMATION_NAME = "Win";

  public static Player GetPlayer()
  {
    return GameObject.FindObjectOfType<Player>();
  }

  public static TimeHandler GetTimeHandler()
  {
    return GameObject.FindObjectOfType<TimeHandler>();
  }

  public static void AnimateWin()
  {
    GameObject.Find(CANVAS_SELECTOR).GetComponent<Animator>().Play(WIN_ANIMATION_NAME);
  }
}