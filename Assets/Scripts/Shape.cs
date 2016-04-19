using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shape : MonoBehaviour
{
  #region Private Fields

  private Hammer _hammer;

  private float _lastAliveTime;

  private bool _lastFrameMouseLeft;

  private bool _lastFrameMouseRight;

  private int _multiplier;

  private Hole _myHole;

  [SerializeField]
  private Points _myPoints = Points.Ten;

  [SerializeField]
  private Type _myType;

  private SpriteRenderer _renderer;

  private Score _score;

  private Image _shapeShift;
  private Image _wrong;

  [SerializeField]
  private float _shiftingProbability = 1f;

  [SerializeField]
  private Sprite[] _sprites;

  [SerializeField]
  private AudioClip[] _hitClips;
  [SerializeField]
  private AudioClip _babyClip;

  [SerializeField]
  private float _propapilityForNotHit = 0.15f;

  private int _curPoints;
  private float _curShiftingPropability;
  private AudioSource _shapeShiftAudio;
  private AudioSource _wrongAudio;
  private AudioSource _source;

  #endregion Private Fields

  #region Public Enums

  public enum Points
  {
    Ten = 10,
    Twenty = 20,
    Thirty = 30,
    Fifty = 50
  }

  public enum Type
  {
    Square,
    Circle,
    Baby
  }

  #endregion Public Enums

  #region Public Methods

  public void DigUp(float currentAliveTime)
  {
    SetSprite();
    _curShiftingPropability = Mathf.Max(0.10f, _curShiftingPropability - 0.10f);
    gameObject.SetActive(true);
    _multiplier++;
    var scaleTime = 0.2f;
    transform.DOScale(Vector3.one, scaleTime);
    transform.DOLocalMove(new Vector3(0, 25, 0), scaleTime);
    StartCoroutine(DigDown(currentAliveTime + scaleTime));
  }

  #endregion Public Methods

  #region Private Methods

  private IEnumerator DigDown(float currentAliveTime)
  {
    _lastAliveTime = Mathf.Max(0.5f, currentAliveTime);
    yield return new WaitForSeconds(_lastAliveTime);
    var scaleTime = 0.2f;
    transform.DOScale(new Vector3(1, 0.1f, 1), scaleTime);
    transform.DOLocalMove(new Vector3(0, -25, 0), scaleTime).OnComplete(Reset);
    if (_myType == Type.Baby && _multiplier != 1 && _curPoints != 0)
    {
      //Baby ofter shifting, score points for not hitting!
      _score.IncrementScore(_curPoints * _multiplier);
    }
    //Reset();
  }

  public void HandleHit(Hammer hammer)
  {
    StopAllCoroutines();
    _curPoints += (int)_myPoints;
    var playClip = _babyClip;
    if (_myType != Type.Baby)
    {
      var rand = Random.Range(0, _hitClips.Length);
      playClip = _hitClips[rand];
    }
    _source.clip = playClip;
    _source.Play();

    transform.localScale = new Vector3(1, 0.4f, 1);
    transform.localPosition = new Vector3(0, -5, 0);
    if (hammer.TargetType != _myType)
    {
      // _wrong.gameObject.SetActive(true);
      _wrongAudio.Play();
      _wrong.DOKill();
      _wrong.DOFade(0.7f, 0.35f).OnComplete(() => _wrong.DOFade(0, 0.35f));
      Camera.main.DOShakePosition(0.75f, new Vector3(7, 7));
      _score.IncrementScore(-_curPoints * _multiplier);
      Reset();
      return;
    }

    var r = Random.Range(0, 1f);
    if (r < _curShiftingPropability)
    {
      //_shapeShift.DOFade(1, 0.25f).OnComplete(() => _shapeShift.DOFade(0, 0.25f));
      //  _shapeShift.gameObject.SetActive(true);
      _shapeShiftAudio.Play();
      _shapeShift.DOKill();
      _shapeShift.DOFade(0.7f, 0.35f).OnComplete(() => _shapeShift.DOFade(0, .35f));
      //_shapeShift.transform.DOPunchScale(new Vector3(10, 10), 0.25f);
      DigUp(_lastAliveTime * 0.90f);
    }
    else
    {
      _score.IncrementScore(_curPoints * _multiplier);
      Reset();
    }
  }

  public void Reset()
  {
    StopAllCoroutines();
    _myHole.InUse = false;
    _multiplier = 0;
    Invoke("Disable", 0.2f);
    _curPoints = 0;
    _curShiftingPropability = _shiftingProbability;
  }

  private void Disable()
  {
    gameObject.SetActive(false);
  }

  private void SetSprite()
  {
    Sprite s;
    int r;
    do
    {
      r = Random.Range(0, _sprites.Length);
      if (r == 6)
      {
        var p = Random.Range(0, 1f);
        if (p > _propapilityForNotHit)
          r = Random.Range(0, _sprites.Length - 1);
      }
      s = _sprites[r];
    } while (s == _renderer.sprite);
    _renderer.sprite = s;
    switch (r)
    {
      case 0:
        _myType = Type.Square;
        _myPoints = Points.Ten;
        break;

      case 1:
        goto case 0;
      case 2:
        goto case 0;
      case 3:
        _myType = Type.Circle;
        _myPoints = Points.Twenty;
        break;

      case 4:
        goto case 3;
      case 5:
        goto case 3;
      case 6:
        _myType = Type.Baby;
        _myPoints = Points.Thirty;
        break;
    }
  }

  // Use this for initialization
  private void Start()
  {
    _score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    Assert.IsNotNull(_score);
    _shapeShift = GameObject.FindGameObjectWithTag("ShapeShift").GetComponent<Image>();
    Assert.IsNotNull(_shapeShift);
    _shapeShiftAudio = _shapeShift.GetComponent<AudioSource>();
    _wrong = GameObject.FindGameObjectWithTag("Wrong").GetComponent<Image>();
    Assert.IsNotNull(_wrong);
    _wrongAudio = _wrong.GetComponent<AudioSource>();
    _renderer = GetComponent<SpriteRenderer>();
    Assert.IsNotNull(_renderer);
    _myHole = transform.parent.GetComponent<Hole>();
    Assert.IsNotNull(_myHole);
    gameObject.SetActive(false);
    _source = GameObject.FindGameObjectWithTag("Board").GetComponent<AudioSource>();
    Assert.IsNotNull(_source);
    _curShiftingPropability = _shiftingProbability;
  }

  #endregion Private Methods
}