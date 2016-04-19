using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
  private Func<float, float> _foreground = x => -1.428571429e-3f * x + 1.392857143f;
  private Func<float, float> _middleground = x => -1.666666667e-3f * x + 1.458333333f;
  private Func<float, float> _background = x => -0.0025f * x + 1.8125f;

  private void Start()
  {
    //TODO DEBUG
    Follow = true;
  }

  // Update is called once per frame
  private void Update()
  {
    if (!Follow) return;
    var nPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    nPos.z = 0;
    transform.position = nPos;
    float scaleFactor = 1;
    var mouseScreenY = Input.mousePosition.y;
    if (mouseScreenY < 100)
    {
      scaleFactor = 1.25f;
    }
    else if (mouseScreenY < 275)
    {
      scaleFactor = _foreground(mouseScreenY);
    }
    else if (mouseScreenY < 425)
    {
      scaleFactor = _middleground(mouseScreenY);
    }
    else if (mouseScreenY < 525)
    {
      scaleFactor = _background(mouseScreenY);
    }
    else
    {
      scaleFactor = 0.5f;
    }
    //print("ScaleFactor: {0}".F(scaleFactor));
    transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
  }

  public bool Follow { get; set; }
}