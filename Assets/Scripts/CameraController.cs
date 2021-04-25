using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  
  private Camera camera;
  private Vector3 lastPoint = Vector3.zero;

  [SerializeField] private Vector3 topLeft, rightBottom;

  [SerializeField] private float cameraMultiplier = .9f;
  //[SerializeField] private int layerMask = 1; 

  private void Start()
  {
    camera = GetComponent<Camera>();
  }

  private void Update()
  {
    var coordinates = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
    print(coordinates);
    var x = Mathf.Lerp(topLeft.x, rightBottom.x, coordinates.x);
    var y = Mathf.Lerp(rightBottom.y, topLeft.y, coordinates.y);
    var newPosition = new Vector3(x, y, 0f);

    transform.LookAt(newPosition, Vector3.up);
  }
}
