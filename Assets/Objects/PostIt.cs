using UnityEngine;

[CreateAssetMenu(fileName = "PostIt", menuName = "Hint/Post It")]
public class PostIt : Hint
{
  public Sprite text;

  public void LogMe()
  {
    //Debug.Log(this.name + ": " + this.description + " (" + this.text + ")");
  }
}