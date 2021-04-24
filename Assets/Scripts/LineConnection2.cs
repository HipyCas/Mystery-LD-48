using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnection : MonoBehaviour
{

  public Camera camera;

  private Transform GetPointInPlane()
  {
    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    RayCastHit hit;
    if(Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
    {
      return hit.transform;
    }
  }
}
