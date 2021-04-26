using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public List<Hint> collectedHints;
  public Node[] objectiveNodes;
  [SerializeField] private List<Node> collectedNodes;
  // Start is called before the first frame update
  void Start()
  {
    collectedHints = new List<Hint>(); // TODO Erase, as in the future there will be hints given at beginning
    collectedNodes = new List<Node>();
  }

  // Update is called once per frame
  void Update()
  {
    //RandomWin();
  }

  public void AddNode(Hint firstHint, Hint secondHint)//, int connectionGameObjectID)
  {
    Node node = new Node((firstHint ?? (new Hint())), (secondHint ?? (new Hint())));//, connectionGameObjectID);
    collectedNodes.Add(node);
  }

  public void RandomWin()
  {
    if (collectedNodes.Count > 2 && Constants.GetTimeHandler().TimeSpan.TotalSeconds > (Constants.GetTimeHandler().JourneyStartTimeSpan.TotalSeconds + 1))
    {
      //Debug.Log("Can randomly win!!! Hurray!!!");
      if (true)//(new System.Random()).Next(5) == 0)
      {
        Constants.AnimateWin();
      }
    }
  }
}
