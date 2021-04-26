using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

  public Hint hint;

  //public List<Node> nodes;

  //private bool hitMe;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    /*
    hitMe = false;

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Clone to the function (checks if the ray is colliding with a connection or a pin)
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) // 100 is the max distance the ray reaches (consumes less resources)
    {
      if (hit.transform.gameObject.name == name) // The prefab has a tag called "Pin"
      {
        hitMe = true;
      }
    }
    else return;

    if (hitMe && Input.GetMouseButtonDown(2))
    {
      foreach (Node node in nodes)
      {
        Destroy(GameObject.Find(node.instanceID.ToString())); // Only works thanks to the renaming in LineConnection2.cs line
      }
    }
    */
  }

}
