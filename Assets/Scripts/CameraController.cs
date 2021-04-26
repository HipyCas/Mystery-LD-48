using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public float zoomScale = 1f;
  private new Camera camera;
  private Vector3 lastPoint = Vector3.zero;

  public int maxFieldOfView = 90;
  public int minFieldOfView = 5;

  [SerializeField] private Vector3 topLeft, rightBottom;

  [SerializeField] private float cameraMultiplier = .9f;
  //[SerializeField] private int layerMask = 1; 

  private void Start()
  {
    //camera = GetComponent<Camera>();
    camera = Camera.main;
  }

  private void Update()
  {
    var coordinates = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
    //print(coordinates);
    var x = Mathf.Lerp(topLeft.x, rightBottom.x, coordinates.x);
    var y = Mathf.Lerp(rightBottom.y, topLeft.y, coordinates.y);
    var newPosition = new Vector3(x, y, 0f);

    transform.LookAt(newPosition, Vector3.up);

    if ((camera.fieldOfView < maxFieldOfView && Input.mouseScrollDelta.y < 0) || (camera.fieldOfView > minFieldOfView && Input.mouseScrollDelta.y > 0)) camera.fieldOfView -= Input.mouseScrollDelta.y * zoomScale;
  }
}
