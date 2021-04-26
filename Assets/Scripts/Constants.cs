
using UnityEngine;

public class Constants : MonoBehaviour
{
  public string RADIO_NEXT_BUTTON_NAME = "NextRadioButton";
  public string RADIO_PAUSE_BUTTON_NAME = "PauseRadioButton";

  public static string CONNECTIONS_PARENT_SELECTOR = "/ConnectionsParent";

  public static string PLAYER_SELECTOR = "/Player";

  public static Player GetPlayer()
  {
    return GameObject.FindObjectOfType<Player>();
  }
}