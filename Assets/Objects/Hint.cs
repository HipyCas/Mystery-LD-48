using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hint", menuName = "Hint/Base")]
public class Hint : ScriptableObject
{
  public new string name;
  public string description;
  
  public Hint[] requiredHints;
  public Node[] requiredNodes;

  public bool CanGet()
  {
    // TODO Method to check wether you have the requirements to get this hint, to be used in locations
    return true;
  }
}