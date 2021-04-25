using System;
using UnityEngine;
//using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

  public Track[] tracks;

  public static MusicManager instance;

  private int playingTrack = 0;

  public bool playLoop = true;

  // Start is called before the first frame update
  void Awake()
  {
    if (instance == null) instance = this;
    else
    {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);

    foreach (Track t in tracks)
    {
      t.source = gameObject.AddComponent<AudioSource>();

      t.source.clip = t.clip;
      t.source.volume = t.volume;
      t.source.pitch = t.pitch;
      t.source.spatialBlend = 1;
    }
  }

  private void Start()
  {
    //if (playLoop) Invoke("Loop", 1f);
    PlayNext();
  }

  private void Update()
  {
    //if (!AnyPlaying() && playLoop) Loop();
    if (!AnyPlaying() && playLoop) PlayNext();
  }

  public void Play(string name)
  {
    Track t = Array.Find(tracks, track => track.name == name);
    if (t == null)
    {
      Debug.LogWarning("Track " + name + " does not exist");
    }
    t.source.Play();
  }

  public void PlayNext()
  {
    tracks[playingTrack++].source.Play();
  }

  public void Loop()
  {
    while (playingTrack != tracks.Length)
    {
      tracks[playingTrack].source.Play();
      while (tracks[playingTrack].source.isPlaying) { }
      playingTrack++;
    }
  }

  public bool AnyPlaying()
  {
    foreach (Track t in tracks)
    {
      if (t.source.isPlaying) return true;
    }
    return false;
  }
}
