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

  private Pin firstPin;
  private Pin lastPin;

  [SerializeField] private float scaler = 1;

  private List<GameObject> lines = new List<GameObject>();

  private GameObject connectionsParent;
  private Player player;

  private bool isDragging = false;
  private bool isPin = false;

  private class PinWrapper
  {
    public Vector3 point;
    public Pin pin;

    public PinWrapper(Vector3 point, GameObject pin)
    {
      this.point = point;
      this.pin = pin.GetComponent<Pin>();
    }
  }

  private void Start()
  {
    connectionsParent = GameObject.Find(Constants.CONNECTIONS_PARENT_SELECTOR);
    player = Constants.GetPlayer() ?? new Player();
  }

  private void Update()
  {

    Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Clone to the function (checks if the ray is colliding with a connection or a pin)
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
    {
      if (hit.transform.gameObject.tag == "Connection" && !isDragging && (Input.GetMouseButtonDown(2) || Input.GetKey("delete"))) // Destroys connection if right clicked
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
    else return;

    if (Input.GetMouseButtonDown(0) && !isDragging && isPin) // If there is no dragging line, start one
    {
      isDragging = true;

      PinWrapper tempWrap = GetPointInPlane();
      if (tempWrap == null) return;
      firstPosition = tempWrap.point;
      firstPin = tempWrap.pin ?? new Pin();

      lines.Add(Instantiate(connectorPrefab, firstPosition, Quaternion.identity));
      lines[index].transform.parent = connectionsParent.transform;
    }

    if (Input.GetMouseButton(0) && isDragging && index < lines.Count && index >= 0) // If player is dragging change the last position of the connector
    {
      PinWrapper tempWrap = GetPointInPlane();
      if (tempWrap == null)
      {
        Destroy(lines[index]);
        isDragging = false;
        isPin = false;
        return;
      }
      lastPosition = tempWrap.point;
      lastPin = tempWrap.pin ?? new Pin();

      lines[index].transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(firstPosition.y - lastPosition.y, firstPosition.x - lastPosition.x) * 180f / Mathf.PI) - 90);
      lines[index].transform.localScale = new Vector3(1, Vector3.Distance(firstPosition, lastPosition) / 2f, 1);
    }

    if (Input.GetMouseButtonUp(0)) // Resetting the position of the positions for next time
    {
      isDragging = false; // Dragging stops

      if (isPin) // If you stop dragging in a pin the connector stops so you can make a new one
      {
        lines[index].name = lines[index].GetInstanceID().ToString();
        player.AddNode(
          (firstPin.hint ?? (new Hint())),
          (lastPin.hint ?? (new Hint()))
        //lines[index].GetInstanceID()
        );

        firstPosition = Vector3.zero;
        lastPosition = Vector3.zero;
        index++;

        player.RandomWin();
      }
      // FIX ME: Check if current index has a line or not, only destroy it when it does
      else if (index < lines.Count) //lines[index] != null) // Else, you will destroy the current line, so you can start a new one (if on corkboard)
      {
        Destroy(lines[index]);
        lines.RemoveAt(index);
      }
    }

  }

  private PinWrapper GetPointInPlane()
  {
    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
    {
      return new PinWrapper(hit.point, hit.transform.gameObject); // Returns the point that hits the raycast
    }
    else // Destroys line if it exists the corkboard
    {
      Destroy(lines[index]);
      isDragging = false;
      isPin = false;
      return null;
    }
  }
}
