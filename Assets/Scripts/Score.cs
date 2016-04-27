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
    // NOTE (goost) Padding Zeroes
    // https://msdn.microsoft.com/en-us/library/dd260048(v=vs.110).aspx
    _currentScore += toAdd;
   _text.text = _currentScore.ToString("D4");
  }

  public void Reset()
  {
    _currentScore = 0;
    _text.text = "0000";
  }
}
