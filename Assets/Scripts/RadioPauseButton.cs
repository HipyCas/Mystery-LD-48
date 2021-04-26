using UnityEngine;

public class RadioPauseButton : MonoBehaviour
{

  public MusicManager musicManager; // Defaults to instance of type musicManager

  private void Start()
  {
    if (musicManager == null) musicManager = GameObject.FindObjectOfType<MusicManager>();
  }

  private void OnMouseDown()
  {
    if (musicManager.isPaused) musicManager.Play();
    else musicManager.Pause();
  }

}