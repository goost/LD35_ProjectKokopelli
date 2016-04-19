using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
  private Text _text;

  private int _currentScore;
  public string FinalScore
  {
    get { return "" + _currentScore; }
  }

  // Use this for initialization
  private void Start()
  {
    _text = GetComponent<Text>();
    _currentScore = 0;
    _text.text = "0000";
  }

  public void IncrementScore(int toAdd)
  {
    _currentScore += toAdd;
    if (_currentScore < 0)
    {
      _text.text = "" + _currentScore;
    }
    else if (_currentScore < 10)
    {
      _text.text = "000" + _currentScore;
    }
    else if (_currentScore < 100)
    {
      _text.text = "00" + _currentScore;
    }
    else if (_currentScore < 1000)
    {
      _text.text = "0" + _currentScore;
    }
    else
    {
      _text.text = "" + _currentScore;
    }
  }

  public void Reset()
  {
    _currentScore = 0;
    _text.text = "0000";
  }
}