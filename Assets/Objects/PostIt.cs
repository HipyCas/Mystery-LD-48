using UnityEngine;

[CreateAssetMenu(fileName = "PostIt", menuName = "Hint/Post It")]
public class PostIt : Hint
{
  public string text;

  void LogMe()
  {
    Debug.Log(this.name + ": " + this.description + " (" + this.text + ")");
  }
}