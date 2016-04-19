using System;
using System.Collections;
using UnityEngine;

public class DoubleHammer : Hammer
{
  [SerializeField]
  private Hammer _rightHammer;
  [SerializeField]
  private Hammer _leftHammer;

  private Action<Hammer> _currentSwingDownAction;
  private int _counter;
  private Action _currentEndAction;

  protected override void Start()
  {
    base.Start();
    Debug.Log("Start in DoubleHammer");
  }

  public override void SwingDown(Action<Hammer> onSwingDownAction, Action onEndAction)
  {
    _counter = 0;
    _currentSwingDownAction = onSwingDownAction;
    _currentEndAction = onEndAction;
    _rightHammer.SwingDown(HandleHammerSwing, HandleEndAction);
    _leftHammer.SwingDown(HandleHammerSwing, HandleEndAction);
  }

  private void HandleHammerSwing(Hammer hammer)
  {
    // hammer.GetComponent<SpriteRenderer>().enabled = false;
    _counter++;
    if (_counter != 2) return;
    _counter = 0;
    //_renderer.enabled = true;
    _currentSwingDownAction(this);
    //Invoke("Reset", 0.15f);
  }

  private void HandleEndAction()
  {
    _counter++;
    if (_counter != 2) return;
    _counter = 0;
    _currentEndAction();
  }

  private void Reset()
  {
    _leftHammer.GetComponent<SpriteRenderer>().enabled = true;
    _rightHammer.GetComponent<SpriteRenderer>().enabled = true;
    _renderer.enabled = false;
  }
}