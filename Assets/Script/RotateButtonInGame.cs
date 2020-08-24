using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateButtonInGame : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool rotateButtonPressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        rotateButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rotateButtonPressed = false;
    }
    public bool RotateButtonIsPressed()
    {
        return rotateButtonPressed;
    }
}
