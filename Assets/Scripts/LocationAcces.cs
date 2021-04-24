using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationAcces : MonoBehaviour
{
  private Player player;
  public Location location;
  // Start is called before the first frame update
  void Start()
  {
    // TODO Move to constants file
    //this.player = GameObject.Find("/Player");
    this.player = GameObject.FindObjectOfType<Player>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void CheckRequirements()
  {
    if (this.player.collectedNodes[0] != this.location.requiredNodes[0]) Debug.Log("Falsy");
  }
}
