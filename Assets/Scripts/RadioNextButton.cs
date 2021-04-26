using UnityEngine;

public class RadioNextButton : MonoBehaviour
{

  public MusicManager musicManager; // Defaults to instance of type musicManager

  private void Start()
  {
    if (musicManager == null) musicManager = GameObject.FindObjectOfType<MusicManager>();
  }

  private void OnMouseDown()
  {
    musicManager.Next();
  }

}