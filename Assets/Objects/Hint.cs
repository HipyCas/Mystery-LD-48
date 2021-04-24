using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hint", menuName = "Hint/Base")]
public class Hint : ScriptableObject
{
  public new string name;
  public string description;
}