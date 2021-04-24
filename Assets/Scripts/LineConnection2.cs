using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnection2 : MonoBehaviour
{

  public new Camera camera;

  private Transform GetPointInPlane()
  {
    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
    {
      return hit.transform;
    }
    else return null;
  }
}
