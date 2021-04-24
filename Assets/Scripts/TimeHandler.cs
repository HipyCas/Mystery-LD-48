using System;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
  public int currentSeconds = 0;
  public int currentDay = 1;
  //public string startTime = "00:00:00";
  public int startDay = 1;
  public string journeyStart = "08:00:00";
  private TimeSpan journeyStartSpan; // Left to implemnet dynamic update for this and below
  public string journeyEnd = "20:00:00";
  private TimeSpan journeyEndSpan;
  public Text timeText;
  public int timeLimitSeconds = -1; // Set to -1 for no time limit
  public float timeSpeed = 1;
  private float lastTimeSpeed; // To detect time speed changes at runtime
  private const float MINIMUM_TIME_SPEED = 0.00001F;
  private const float MAXIMUM_TIME_SPEED = 99999F;

  // Start is called before the first frame update
  void Start()
  {
    this.ResetJourney();

    this.journeyStartSpan = TimeSpan.Parse(this.journeyStart);
    this.journeyEndSpan = TimeSpan.Parse(this.journeyEnd);

    if (this.timeSpeed < MINIMUM_TIME_SPEED) this.timeSpeed = MINIMUM_TIME_SPEED;
    else if (this.timeSpeed > MAXIMUM_TIME_SPEED) this.timeSpeed = MAXIMUM_TIME_SPEED;

    InvokeRepeating("IncreaseSeconds", 0f, (1f / this.timeSpeed));
    this.lastTimeSpeed = timeSpeed;
  }

  // Update is called once per frame
  void Update()
  {
    if (this.timeSpeed != this.lastTimeSpeed)
    {
      this.StopTime();
      this.Start();
    }

    TimeSpan ts = TimeSpan.FromSeconds(currentSeconds);
    Debug.Log("+ts: " + ts.Seconds + ", real: " + this.currentSeconds + ", journeyEnd" + this.journeyEndSpan.TotalSeconds);
    if (this.currentSeconds >= this.journeyEndSpan.TotalSeconds)
    {
      this.ResetJourney();
      this.currentDay += 1;
    }

    //this.timeText.text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString() + " -> " + this.currentSeconds.ToString() + "s";
    this.timeText.text = "Day " + this.currentDay + " " + ts.ToString("hh\\:mm\\:ss");

    if (this.currentSeconds >= this.timeLimitSeconds && this.timeLimitSeconds >= 0)
    {
      this.timeText.color = new Color(255, 0, 0);
    }
  }

  public void IncreaseSeconds()
  {
    this.currentSeconds += 1;
  }

  public void StopTime()
  {
    CancelInvoke("IncreaseSeconds");
  }

  public void StartTime()
  {
    InvokeRepeating("IncreaseSeconds", 0f, (1f / this.timeSpeed));
  }

  public void StartTime(float speed)
  {
    InvokeRepeating("IncreaseSeconds", 0f, (1f / speed));
  }

  public void ResetJourney()
  {
    TimeSpan ts = TimeSpan.Parse(this.journeyStart);
    this.currentSeconds = (int)ts.TotalSeconds;
  }
}
