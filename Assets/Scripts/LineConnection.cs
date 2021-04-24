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

  // Update is called once per frame
  void Update()
  {
    if (this.beingHeldDown)
    {
      Vector3 mousePos;
      mousePos = Input.mousePosition;
      mousePos = new Vector3(mousePos.x, mousePos.y, this.zPosition);
      mousePos = Camera.main.ScreenToWorldPoint(mousePos, 0); // ! Causing mouse position to reset

      this.finalPos = new Vector3(mousePos.x, mousePos.y, this.zPosition);

      float angle = Mathf.Atan2(this.finalPos.y - this.startPos.y, this.finalPos.x - this.startPos.x);

      this.connectorObject.transform.localScale = new Vector3(this.xScale, this.finalPos.y - this.startPos.y, this.zScale);
      this.connectorObject.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
  }

  private void OnMouseDown()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Vector3 mousePos;
      mousePos = Input.mousePosition;
      mousePos = Camera.main.ScreenToWorldPoint(mousePos, 0);

      this.startPos = new Vector3(mousePos.x, mousePos.y, this.zPosition);

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
}
