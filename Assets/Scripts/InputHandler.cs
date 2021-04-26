using System.Collections;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

  private void Update()
  {
    if (Input.GetKey("escape")) Application.Quit();
  }

}