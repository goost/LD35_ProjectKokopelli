using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorBreath : MonoBehaviour
{
  [SerializeField]
  private Color _toColor;

  [SerializeField]
  private float _speed;

  private Color _fromColor;

  private Text _text;

  private void Start()
  {
    _text = GetComponent<Text>();
    _text.DOColor(_toColor, _speed).SetLoops(-1, LoopType.Yoyo);
  }

  private void OnEnable()
  {
    _text.DOTogglePause();
  }

  private void OnDisable()
  {
    _text.DOTogglePause();
  }
}