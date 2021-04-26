using System;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
  //public int currentSeconds = 0;
  private int currentDay = 1;
  //public string startTime = "00:00:00";
  private TimeSpan ts;
  public int startDay = 1;
  public string journeyStartParseable = "08:00:00";
  private TimeSpan journeyStart; // Left to implemnet dynamic update for this and below
  public string journeyEndParseable = "20:00:00";
  private TimeSpan journeyEnd;
  public Text timeText;
  //public int timeLimitSeconds = -1; // Set to -1 for no time limit
  //public int timeLimitDays = 0;
  public string timeLimitParseable;
  private TimeSpan timeLimit;
  public float timeSpeed = 1;
  private float lastTimeSpeed; // To detect time speed changes at runtime
  private const float MINIMUM_TIME_SPEED = 0.00001F;
  private const float MAXIMUM_TIME_SPEED = 99999F;
  [SerializeField] private GameObject hourHand;
  [SerializeField] private GameObject minuteHand;

  public TimeSpan TimeSpan
  {
    get
    {
      return ts;
    }
  }

  public TimeSpan JourneyStartTimeSpan
  {
    get { return journeyStart; }
  }

  // Start is called before the first frame update
  void Start()
  {
    // Obtain clock hands
    hourHand = GameObject.Find("HourHand"); // ? Change to "/Clock/HourHand"
    minuteHand = GameObject.Find("MinuteHand"); // ? Change to "/Clock/MinuteHand"
    // TODO Move string identifiers to project-wide constants

    // Parse time spans
    TimeSpan.TryParse(this.journeyStartParseable, out this.journeyStart);
    TimeSpan.TryParse(this.journeyEndParseable, out this.journeyEnd);
    TimeSpan.TryParse(this.timeLimitParseable, out this.timeLimit);

    this.ResetJourney();

    if (this.startDay < 1) this.currentDay = 1;
    else this.currentDay = this.startDay;

    this.FixSpeed();

    InvokeRepeating("IncreaseSeconds", 0f, (1f / this.timeSpeed));
    this.lastTimeSpeed = timeSpeed;
  }

  // Update is called once per frame
  void Update()
  {
    hourHand.transform.rotation = Quaternion.Euler(hourHand.transform.rotation.x, hourHand.transform.rotation.y, -((360f * 2f) * (float)(ts.TotalDays - (double)ts.Days)));
    minuteHand.transform.rotation = Quaternion.Euler(minuteHand.transform.rotation.x, minuteHand.transform.rotation.y, -(360f * (float)(ts.TotalHours - (double)ts.Hours)));

    if (this.timeSpeed != this.lastTimeSpeed)
    {
      this.StopTime();
      this.Start();
    }

    //TimeSpan ts = TimeSpan.FromSeconds(currentSeconds);
    if (this.ts.TotalSeconds >= this.journeyEnd.TotalSeconds) this.NextJourney();

    //this.timeText.text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString() + " -> " + this.currentSeconds.ToString() + "s";
    this.timeText.text = "Day " + this.currentDay + " " + this.ts.ToString("hh\\:mm\\:ss");

    // ? Make relative to journey start or absolute? Prob absolute better
    // TODO take day from timeLimit ¿and add 1 to it? and compare with current day
    if (this.ts.CompareTo(this.timeLimit) == 1 && this.timeLimit.CompareTo(TimeSpan.Zero) != 0)
    {
      this.timeText.color = new Color(255, 0, 0);
    }
  }

  public void IncreaseSeconds()
  {
    //this.currentSeconds += 1;
    this.ts = this.ts.Add(new TimeSpan(0, 0, 1));
  }

  public void StopTime()
  {
    CancelInvoke("IncreaseSeconds");
  }

  public void StartTime()
  {
    this.FixSpeed();
    InvokeRepeating("IncreaseSeconds", 0f, (1f / this.timeSpeed));
  }

  public void StartTime(float speed)
  {
    InvokeRepeating("IncreaseSeconds", 0f, (1f / speed));
  }

  private void FixSpeed()
  {
    //if (this.timeSpeed < MINIMUM_TIME_SPEED) this.timeSpeed = MINIMUM_TIME_SPEED;
    if (this.timeSpeed < 0) this.timeSpeed = 0;
    if (this.timeSpeed > MAXIMUM_TIME_SPEED) this.timeSpeed = MAXIMUM_TIME_SPEED;
  }

  public float FixSpeed(float speed)
  {
    if (speed < 0) return 0;
    if (speed > MAXIMUM_TIME_SPEED) return MAXIMUM_TIME_SPEED;
    return speed;
  }

  public void ResetJourney()
  {
    //TimeSpan ts = TimeSpan.Parse(this.journeyStartParseable);
    //this.currentSeconds = (int)ts.TotalSeconds
    this.ts = this.journeyStart;
  }

  public void NextJourney()
  {
    this.ResetJourney();
    this.currentDay++;
  }
}
