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
      Debug.Log(mousePos);
      mousePos = new Vector3(mousePos.x, mousePos.y, this.zPosition);
      mousePos = Camera.main.ScreenToWorldPoint(mousePos, 0); // ! Causing mouse position to reset
      Debug.Log("After camera: " + mousePos);

      this.finalPos = new Vector3(mousePos.x, mousePos.y, this.zPosition);
      Debug.Log("This is the final position rn: " + this.finalPos + ", compared to start one: " + this.startPos);

      this.connectorObject.transform.localScale = new Vector3(this.xScale, this.finalPos.y - this.startPos.y, this.zScale);
      Debug.Log("Scaled to " + this.connectorObject.transform.localScale);
    }
  }

  private void OnMouseDown()
  {
    Debug.Log("At least, some mouse down happened");
    if (Input.GetMouseButtonDown(0))
    {
      Debug.Log("Mouse down");
      Vector3 mousePos;
      mousePos = Input.mousePosition;
      Debug.Log(mousePos);
      mousePos = Camera.main.ScreenToWorldPoint(mousePos);

      this.startPos = new Vector3(mousePos.x, mousePos.y, this.zPosition);

      this.connectorObject = Instantiate(this.connectorPrefab, this.startPos, Quaternion.identity);
      this.connectorObject.transform.localScale = this.finalPos - this.startPos;
      this.connectorObject.transform.parent = transform;
      Debug.Log("Got this instance: " + this.connectorObject);

      this.beingHeldDown = true;
    }
  }
  private void OnMouseUp()
  {
    this.beingHeldDown = false;
  }
}
