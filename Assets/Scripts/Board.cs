using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
  [SerializeField]
  private GameObject _pointer;
  [SerializeField]
  private GameObject _button;
  [SerializeField]
  private Text _time;

  [SerializeField]
  private Text _endText;
  [SerializeField]
  private int _playTime;

  [SerializeField]
  private Score _score;
  [SerializeField]
  private Vector2 _inBetweenTimeBounds;
  [SerializeField]
  private float _initialAliveTime;
  [SerializeField]
  private AnimationCurve _timeChange;
  [SerializeField]
  private Hole[] _holes;

  private float _currentAliveTime;
  private float _currentTime;
  private Vector2 _currentInBetweenTimeBounds;
  private bool _running;
  [SerializeField]
  private GameObject _menuBackground;
  [SerializeField]
  private GameObject _endScreen;

  [SerializeField]
  private GameObject _shapeShift;

  // Use this for initialization
  private void Start()
  {
    _time.text = "" + _playTime;
  }

  private IEnumerator DecrementTime()
  {
    var currentPlayTime = _playTime;
    while (currentPlayTime > 0)
    {
      if (!_running) continue;
      //yield return null;
      yield return new WaitForSeconds(1);
      currentPlayTime -= 1;
      _time.text = "" + currentPlayTime;
    }
    _running = false;
    Debug.Log("Game Ended!");
    //_time.text = "END";
    _endText.text = _score.FinalScore;
    _endScreen.SetActive(true);
    _endScreen.GetComponent<AudioSource>().Play();
    _menuBackground.SetActive(true);
    //For avoiding accident restarts
    Invoke("ButtonActive", 2f);
    _pointer.SetActive(false);
    foreach (var hole in _holes)
    {
      hole.Reset();
    }
  }

  private void ButtonActive()
  {
    _button.SetActive(true);
  }

  public void StartGame()
  {
    //_shapeShift.SetActive(false);
    _currentAliveTime = _initialAliveTime;
    _currentInBetweenTimeBounds = _inBetweenTimeBounds;
    _currentTime = 0;
    _score.Reset();
    _running = true;
    StartCoroutine(Spawner());
    StartCoroutine(GameLoop());
    StartCoroutine(DecrementTime());
  }

  private IEnumerator Spawner()
  {
    yield return new WaitForSeconds(0.25f);
    while (_running)
    {
      var r = 0;
      do
      {
        r = Random.Range(0, _holes.Length);
      }
      while (!_holes[r].Spawn(_currentAliveTime));
      yield return new WaitForSeconds(Random.Range(_currentInBetweenTimeBounds.x, _currentInBetweenTimeBounds.y));
    }
  }

  // Update is called once per frame
  private IEnumerator GameLoop()
  {
    while (_running)
    {
      _currentTime += Time.deltaTime / _playTime;
      var eval = _timeChange.Evaluate(_currentTime);
      _currentAliveTime = Mathf.Max(_initialAliveTime * eval, 0.5f);
      _currentInBetweenTimeBounds.x = Mathf.Max(_inBetweenTimeBounds.x * eval, 0.05f);
      _currentInBetweenTimeBounds.y = Mathf.Max(_inBetweenTimeBounds.y * eval, 0.5f);
      yield return null;
      //if (Math.Abs(Time.time % 1) < 0.1f)
      //   Debug.Log("Time: {0}, Eval: {1}, currentTime: {2}".F(Time.time, eval, _currentTime));
    }
  }
}