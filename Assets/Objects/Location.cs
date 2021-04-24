using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Location/Base")]
public class Location : ScriptableObject
{
  public new string name;
  public string description;
  public Hint[] hints;
  public Sprite picture;
  public Node[] requiredNodes;

  public Hint GetHint()
  {
    return this.hints[(new System.Random()).Next(this.hints.Length)]; // Get a random hint
  }
}