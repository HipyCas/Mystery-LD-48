using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnection2 : MonoBehaviour
{

  public new Camera camera;
  public GameObject connectorPrefab;

  private Vector3 firstPosition;
  private Vector3 lastPosition;
  private int index;

  [SerializeField] private float scaler = 1;

  private List<GameObject> lines = new List<GameObject>();

  private void Update()
  {
    if (Input.GetMouseButtonDown(0) && firstPosition == Vector3.zero)
    {
      firstPosition = GetPointInPlane();
      lines.Add(Instantiate(connectorPrefab, firstPosition, Quaternion.Identity));
    }

    if (Input.GetMouseButton(0))
    {
      lastPosition = GetPointInPlane();
      lines[index].rotation = Quaternion.Euler(0, 0, atan2(firstPosition.z - lastPosition.z, firstPosition.x - lastPosition.x) * 180 / PI);
      lines[index].scale = Vector3(1, Vector3.Distance(firstPosition, lastPosition), 1);
    }

    if (Input.GetMouseButtonUp(0)) // Resetting the position of the positions for next time
    {
      firstPosition = Vector3.zero;
      lastPosition = Vector3.zero;
      index++;
    }

  }

  private Vector3 GetPointInPlane()
  {
    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
    {
      return hit.transform.position;
    }
    else return null;
  }
}
