using System.Collections.Generic;
using System;
using UnityEngine;
//using UnityEngine.Audio;

[Serializable]
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

  public bool isPaused = false; // ! YOU SHOULD IN NO WAY USE THE FIELD, IT IS JUST THAT, FOR SOME REASON, THE PROPERTY BELOW DOESN'T WORK
  public bool IsPaused { get; set; }

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
    if (!AnyPlaying() && playLoop && !isPaused)
    {
      PlayNext();
    }
    if (!AnyPlaying() && playRandom && !isPaused)
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
    isPaused = false;
  }

  public void Play(Track track)
  {
    track.source.Play();
    isPaused = false;
  }

  private void PlayNext()
  {
    tracks[playingTrack].source.Play();
    if (playingTrack == tracks.Length - 1) playingTrack = 0;
    else playingTrack++;
    isPaused = false;
  }

  private void Loop()
  {
    while (playingTrack != tracks.Length)
    {
      tracks[playingTrack].source.Play();
      isPaused = false;
      while (tracks[playingTrack].source.isPlaying) { }
      playingTrack++;
    }
  }

  private void PlayRandomNoRepeat()
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
    isPaused = false;
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

  private void PausePlaying()
  {
    Debug.Log("Going to pause " + GetPlaying().name + " as it is definetly playing (that is " + AnyPlaying() + ")");
    if (AnyPlaying())
    {
      GetPlaying().source.Pause();
      Debug.Log("I just paused the song, so now if you say there are any playing that's " + AnyPlaying());
      isPaused = true;
    }
  }

  private void UnPausePlaying()
  {
    Debug.Log("Ok! Whatever you want! I'm unpausing it then");
    //if (!AnyPlaying()) GetPlaying().source.UnPause(); // ! This will never happen I guess
    /*else*/
    lastPlayed.source.UnPause();
    isPaused = false; // TODO This can cause probably many bugs
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

  public void Pause()
  {
    Debug.Log("Is paused? " + isPaused + ". Not any playing? " + !AnyPlaying());
    if (!AnyPlaying() || isPaused) return;
    else PausePlaying();
    isPaused = true;
  }

  public void Play()
  {

    if (AnyPlaying() || !isPaused)
    {
      Debug.Log("Don't what the hell you're trying, I'm just returning");
      return;
    } // Song playing
    else if (isPaused) UnPausePlaying(); // Song paused
    else Next(); // Song stopped
    isPaused = false;
  }

  public void Next()
  {
    if (AnyPlaying()) GetPlaying().source.Stop();
    if (playRandom) PlayRandomNoRepeat();
    else PlayNext();
  }
}
