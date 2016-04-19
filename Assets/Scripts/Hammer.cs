using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Hammer : MonoBehaviour
{
  [SerializeField]
  protected Shape.Type _myTargetType = Shape.Type.Baby;

  [SerializeField]
  protected float _swingTime = 0.2f;

  protected SpriteRenderer _renderer;
  protected bool _isPlayingAnimation;
  private float _factor = 1;

  protected virtual void Start()
  {
    _renderer = GetComponent<SpriteRenderer>();
    Assert.IsNotNull(_renderer);
    switch (_myTargetType)
    {
      case Shape.Type.Square:
        _factor = -1;
        break;

      case Shape.Type.Circle:
        break;

      case Shape.Type.Baby:
        break;

      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  public Shape.Type TargetType
  {
    get { return _myTargetType; }
  }

  public virtual void SwingDown(Action<Hammer> onSwingDownAction, Action onEndAction)
  {
    if (_isPlayingAnimation) return;
    _isPlayingAnimation = true;
    DOTween.Sequence().Append(transform.DOLocalRotate(new Vector3(0, 0, _factor * 55f), _swingTime)).
      AppendCallback(() => onSwingDownAction(this))
      //.AppendInterval(0.2f)
      //.AppendCallback(() => _renderer.enabled = true)
      .Append(transform.DOLocalRotate(Vector3.zero, _swingTime))
      .AppendCallback(() => _isPlayingAnimation = false)
      .AppendCallback(() => onEndAction());
  }
}