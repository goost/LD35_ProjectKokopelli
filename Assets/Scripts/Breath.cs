using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Breath : MonoBehaviour
{
  [SerializeField]
  private Vector3 _scale = new Vector3(1.25f, 1.25f);
  [SerializeField]
  private float _speed = 5f;

  // Use this for initialization
  private void Start()
  {
    transform.DOScale(_scale, _speed).SetLoops(-1, LoopType.Yoyo);
  }

  private void OnEnable()
  {
    transform.DOTogglePause();
  }

  private void OnDisable()
  {
    transform.DOTogglePause();
  }
}