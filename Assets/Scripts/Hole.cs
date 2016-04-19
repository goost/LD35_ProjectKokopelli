using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{
  [SerializeField]
  private Shape[] _shapes;

  public bool InUse { get; set; }

  // Use this for initialization
  private void Start()
  {
  }

  // Update is called once per frame
  private void Update()
  {
  }

  public void Reset()
  {
    foreach (var shape in _shapes)
    {
      shape.Reset();
    }
  }

  public bool Spawn(float currentAliveTime)
  {
    if (InUse) return false;
    var r = Random.Range(0, _shapes.Length);
    _shapes[r].DigUp(currentAliveTime);
    InUse = true;
    return true;
  }
}