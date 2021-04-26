using UnityEngine;

[CreateAssetMenu(fileName = "Node", menuName = "Node")]
public class Node : ScriptableObject
{
  public Hint first;
  public Hint second;
  public readonly string identifer;

  public int instanceID;

  public Node(Hint firstHint, Hint secondHint, int connectionGameObjectID)
  {
    first = firstHint;
    second = secondHint;
    identifer = first.name + "_" + second.name;

    instanceID = connectionGameObjectID;
  }
}