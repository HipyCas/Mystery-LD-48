using System.Collections.Generic;
using System;
using System.Collections;
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

  public float startFadeDuration = 0f;

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
    if (AnyPlaying())
    {
      GetPlaying().source.volume = 0;
      FadeIn(startFadeDuration);
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
    if (AnyPlaying())
    {
      GetPlaying().source.Pause();
      isPaused = true;
    }
  }

  private void UnPausePlaying()
  {
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
    if (!AnyPlaying() || isPaused) return;
    else PausePlaying();
    isPaused = true;
  }

  public void Play()
  {

    if (AnyPlaying() || !isPaused)
    {
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

  private static IEnumerator StartFade(Track track, float duration, float targetVolume)
  {
    float currentTime = 0;
    float start = track.source.volume;

    while (currentTime < duration)
    {
      currentTime += Time.deltaTime;
      track.source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
      yield return null;
    }
    yield break;
  }

  public void FadeIn(float duration)
  {
    StartCoroutine(StartFade(GetPlaying(), duration, GetPlaying().volume));
  }
}
