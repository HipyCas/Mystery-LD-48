using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostItItem : MonoBehaviour
{
  public PostIt asset;
  public Text postitText;
  void Start()
  {
    //this.postitText.text = asset.text;
    this.asset.LogMe();
  }
}