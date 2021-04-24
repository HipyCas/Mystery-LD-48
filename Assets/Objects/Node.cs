using UnityEngine;

[CreateAssetMenu(fileName = "Node", menuName = "Node")]
public class Node : ScriptableObject
{
  public Hint first;
  public Hint second;
  public readonly string identifer;

  public Node()
  {
    this.identifer = first.name + "_" + second.name;
  }
}