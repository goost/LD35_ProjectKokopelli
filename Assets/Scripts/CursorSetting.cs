using System.Collections;
using UnityEngine;

public class CursorSetting : MonoBehaviour
{
  [SerializeField]
  private Texture2D _cursorTexture;
  [SerializeField]
  private CursorMode _cursorMode = CursorMode.Auto;
  [SerializeField]
  private Vector2 _hotSpot = Vector2.zero;

  private void OnMouseEnter()
  {
    Cursor.SetCursor(_cursorTexture, _hotSpot, _cursorMode);
  }

  private void OnMouseExit()
  {
    Cursor.SetCursor(null, Vector2.zero, _cursorMode);
  }
}