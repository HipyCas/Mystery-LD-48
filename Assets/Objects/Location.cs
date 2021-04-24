using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Location/Base")]
public class Location : ScriptableObject
{
  public new string name;
  public string description;
  public Hint[] hints;
  public Sprite picture;
  public Node[] requiredNodes;
  public Hint[] requiredHints;

  public Hint GetHint()
  {
    return this.hints[(new System.Random()).Next(this.hints.Length)]; // Get a random hint
  }

  /// <summary> Return true if all required hints and nodes are provided </summary>
  public bool CanAccess(Hint[] collectedHints, Node[] collectedNodes)
  {
    List<bool> allProvided = new List<bool>();
    // Check for Node requirements, iter over provided for each requirement
    foreach (Node requiredNode in requiredNodes)
    {
      bool isProvided = false; // save if this required node is provided
      foreach (Node collectedNode in collectedNodes)
      {
        if (requiredNode.identifer == collectedNode.identifer)
        {
          isProvided = true; // Tell that this one is actually provided
          break; // And break
        }
      }
      allProvided.Add(isProvided); // append to full requirement list
    }

    // Same as above
    foreach (Hint requiredHint in requiredHints)
    {
      bool isProvided = false;
      foreach (Hint collectedHint in collectedHints)
      {
        // TODO Get identifier for Hint object instead of checking for name?
        if (requiredHint.name == collectedHint.name)
        {
          isProvided = true;
          break;
        }
      }
      allProvided.Add(isProvided);
    }

    if (allProvided.Contains(false)) return false; // Return false if at least one false is in all provided list (if at least one was not provided)
    else return true; // else if all true, return true
  }
}