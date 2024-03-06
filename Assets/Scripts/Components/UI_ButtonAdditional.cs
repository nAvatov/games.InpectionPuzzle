using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonAdditional : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
    public readonly ReactiveProperty<bool> IsDown = new ReactiveProperty<bool>(false);

    public void OnPointerDown(PointerEventData eventData) {
        IsDown.Value = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        IsDown.Value = false;
    }
}
