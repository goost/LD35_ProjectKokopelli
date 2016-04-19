using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class InputHandler : MonoBehaviour
{
  [SerializeField]
  private Hammer _leftHammer;
  [SerializeField]
  private Hammer _rightHammer;

  private bool _curRightClick;
  private bool _curLeftClick;

  private FollowMouse _follower;
  private bool _actionForbidden;

  private void Start()
  {
    _follower = GetComponent<FollowMouse>();
    Assert.IsNotNull(_follower);
  }

  // Update is called once per frame
  private void Update()
  {
    ///*
    if (Input.GetMouseButtonDown(0))
    {
      _curLeftClick = true;
    }
    if (Input.GetMouseButtonUp(0))
    {
      _curLeftClick = false;
    }
    if (Input.GetMouseButtonDown(1))
    {
      _curRightClick = true;
    }
    if (Input.GetMouseButtonUp(1))
    {
      _curRightClick = false;
    }
    //*/
    //_curLeftClick = Input.GetMouseButton(0);
    //_curRightClick = Input.GetMouseButton(1);
    HandleInput();
  }

  private void HandleInput()
  {
    if (_actionForbidden) return;
    if (!_curLeftClick && !_curRightClick) return;
    Hammer selectedHammer = null;
    if (_curLeftClick)
    {
      selectedHammer = _leftHammer;
    }
    else if (_curRightClick)
    {
      selectedHammer = _rightHammer;
    }
    _actionForbidden = true;
    selectedHammer.SwingDown(OnSwingDown, () => _actionForbidden = false);
  }

  private void OnSwingDown(Hammer h)
  {
    //Camera.main.DOShakePosition(0.50f, new Vector3(3f, 3f, 0f));
    //sdsdsdsd
    var mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    var hit = Physics2D.Raycast(new Vector2(mpos.x, mpos.y), Vector2.zero);
    if (hit.collider)
    {
      var shape = hit.collider.GetComponent<Shape>();
      if (shape)
      {
        shape.HandleHit(h);//
      }
    }
  }
}