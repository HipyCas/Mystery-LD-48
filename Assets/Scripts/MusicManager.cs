using System.Collections.Generic;
using System;
using UnityEngine;
//using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

  public Track[] tracks;

  public static MusicManager instance;

  private int playingTrack = 0;

  public bool playLoop = false;

  public bool playRandom = false;
  [SerializeField] private List<Track> playedTracks;
  [SerializeField] private Track lastPlayed;

  public Track LastPlayed { get; }

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
    //PlayNext();
  }

  private void Update()
  {
    if (playedTracks.Count == tracks.Length)
    {
      playedTracks.RemoveAll(track => true);
    }

    //if (!AnyPlaying() && playLoop) Loop();
    if (!AnyPlaying() && playLoop)
    {
      PlayNext();
    }
    if (!AnyPlaying() && playRandom)
    {
      PlayRandomNoRepeat();
    }
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

  public void Play(Track track)
  {
    track.source.Play();
  }

  public void PlayNext()
  {
    tracks[playingTrack].source.Play();
    if (playingTrack == tracks.Length - 1) playingTrack = 0;
    else playingTrack++;
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

  public void PlayRandomNoRepeat()
  {
    System.Random rand = new System.Random();
    Track track;
    do
    {
      track = tracks[rand.Next(tracks.Length)];
    } while (Array.Find<Track>(playedTracks.ToArray(), t => track.name == t.name) != null || lastPlayed.name == track.name);
    playedTracks.Add(track);
    lastPlayed = track;
    track.source.Play();
  }

  public bool AnyPlaying()
  {
    foreach (Track t in tracks)
    {
      if (t.source.isPlaying) return true;
    }
    return false;
  }

  public Track GetPlaying()
  {
    foreach (Track t in tracks)
    {
      if (t.source.isPlaying) return t;
    }
    return null;
  }

  public void PausePlaying()
  {
    if (AnyPlaying()) GetPlaying().source.Pause();
  }

  public void UnPausePlaying()
  {
    if (!AnyPlaying()) GetPlaying().source.UnPause(); // ! This will never happen I guess
    else lastPlayed.source.UnPause();
  }

  public void StopPlaying()
  {
    if (AnyPlaying()) GetPlaying().source.Stop();
    else lastPlayed.source.Stop();
  }

  public void PlayLast()
  {
    if (!AnyPlaying()) lastPlayed.source.Play();
    else Debug.LogWarning("There is already a track playing, cannot play last");
  }
}
