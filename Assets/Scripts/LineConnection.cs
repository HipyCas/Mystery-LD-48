using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnection : MonoBehaviour
{

  private bool beingHeldDown;
  private Vector3 startPos;
  private Vector3 finalPos;
  public GameObject connectorPrefab;
  private GameObject connectorObject;
  public float zPosition;
  public float xScale = 0.1f;
  public float zScale = 0.1f;
  public new Camera camera = null; // Overrides default Component.camera

  void Start()
  {
    if (this.camera == null) this.camera = Camera.main;
  }

  // Update is called once per frame
  void Update()
  {
    if (this.beingHeldDown)
    {
      Transform mousePos = this.GetPointInPlane();

      this.finalPos = new Vector3(mousePos.position.x, mousePos.position.y, this.zPosition);

      float angle = Mathf.Atan2(this.finalPos.y - this.startPos.y, this.finalPos.x - this.startPos.x);

      this.connectorObject.transform.localScale = new Vector3(this.xScale, this.finalPos.y - this.startPos.y, this.zScale);
      this.connectorObject.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
  }

  private void OnMouseDown()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Transform mouseTransform = this.GetPointInPlane();

      this.startPos = new Vector3(mouseTransform.position.x, mouseTransform.position.y, this.zPosition);

      this.connectorObject = Instantiate(this.connectorPrefab, this.startPos, Quaternion.identity);
      this.connectorObject.transform.localScale = new Vector3(this.xScale, 0f, this.zScale);
      this.connectorObject.transform.parent = transform;

      this.beingHeldDown = true;
    }
  }
  private void OnMouseUp()
  {
    this.beingHeldDown = false;
  }

  private Transform GetPointInPlane()
  {
    Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
      return hit.transform;
    else
      return null;
  }
}
