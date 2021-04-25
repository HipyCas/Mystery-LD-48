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

  [SerializeField] private GameObject connectionsParent;

  private void Start()
  {
    connectionsParent = GameObject.Find("/ConnectionsParent");
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0) && firstPosition == Vector3.zero)
    {
      firstPosition = GetPointInPlane();
      lines.Add(Instantiate(connectorPrefab, firstPosition, Quaternion.identity));
      lines[index].transform.parent = connectionsParent.transform;
    }

    if (Input.GetMouseButton(0))
    {
      lastPosition = GetPointInPlane();
      lines[index].transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(firstPosition.y - lastPosition.y, firstPosition.x - lastPosition.x) * 180f / Mathf.PI) - 90);
      lines[index].transform.localScale = new Vector3(1, Vector3.Distance(firstPosition, lastPosition) / 2f, 1);
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
      return hit.point;
    }
    else
    {
      Destroy(lines[index]);
      index--;
      return Vector3.zero;
    }
  }
}
