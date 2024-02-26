using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonAdditional : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
    private bool _isDown;
    public bool IsDown => _isDown;

    public void OnPointerDown(PointerEventData eventData) {
        _isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        _isDown = false;
    }
}
