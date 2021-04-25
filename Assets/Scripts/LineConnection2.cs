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

  private GameObject connectionsParent;

  private bool isDragging = false;
  private bool isPin = false;

  private void Start()
  {
    connectionsParent = GameObject.Find("/ConnectionsParent");
  }

  private void Update()
  {

    Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Clone to the function (checks if the ray is colliding with a connection or a pin)
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
    {
      if (hit.transform.gameObject.tag == "Connection" && !isDragging && Input.GetMouseButtonDown(2)) // Destroys connection if right clicked
      {
        Destroy(hit.transform.gameObject);
      }

      if (hit.transform.gameObject.tag == "Pin") // The prefab has a tag called "Pin"
      {
        isPin = true;
      }
      else
      {
        isPin = false;
      }
    }

    if (Input.GetMouseButtonDown(0) && !isDragging && isPin) // If there is no dragging line, start one
    {
      isDragging = true;
      firstPosition = GetPointInPlane();
      lines.Add(Instantiate(connectorPrefab, firstPosition, Quaternion.identity));
      lines[index].transform.parent = connectionsParent.transform;
    }

    if (Input.GetMouseButton(0) && isDragging) // If player is dragging change the last position of the connector
    {
      lastPosition = GetPointInPlane();
      lines[index].transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(firstPosition.y - lastPosition.y, firstPosition.x - lastPosition.x) * 180f / Mathf.PI) - 90);
      lines[index].transform.localScale = new Vector3(1, Vector3.Distance(firstPosition, lastPosition) / 2f, 1);
    }

    if (Input.GetMouseButtonUp(0)) // Resetting the position of the positions for next time
    {
      isDragging = false; // Dragging stops

      if (isPin) // If you stop dragging in a pin the connector stops so you can make a new one
      {
        firstPosition = Vector3.zero;
        lastPosition = Vector3.zero;
        index++;
      }
      // FIX ME: Check if current index has a line or not, only destroy it when it does
      else if (index < lines.Count) //lines[index] != null) // Else, you will destroy the current line, so you can start a new one (if on corkboard)
      {
        Destroy(lines[index]);
      }
    }

  }

  private Vector3 GetPointInPlane()
  {
    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
    {
      return hit.point; // Returns the point that hits the raycast
    }
    else // Destorys line if it exists the corkboard
    {
      Destroy(lines[index]);
      isDragging = false;
      return Vector3.zero;
    }
  }
}
