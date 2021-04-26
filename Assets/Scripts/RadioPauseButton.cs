using UnityEngine;

public class RadioPauseButton : MonoBehaviour
{

  public MusicManager musicManager; // Defaults to instance of type musicManager

  private void Start()
  {
    if (musicManager == null) musicManager = GameObject.FindObjectOfType<MusicManager>();
    Debug.Log("Setup pause/play button for music manager " + musicManager + " and got that it is " + !musicManager.IsPaused + " playing");
  }

  private void OnMouseDown()
  {
    Debug.Log("I won't let you choose. Song is paused");
    if (musicManager.isPaused) musicManager.Play();
    else musicManager.Pause();
  }

}