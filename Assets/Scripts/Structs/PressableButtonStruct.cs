using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

[Serializable]
public struct PressableButtonStruct {
    public Vector3 _unpressedPosition;
    public Vector3 _pressedPosition;
    public float _pressAnimationDuration;
    public Collider ButtonCollider;
    public System.Action PressCallback;

    public void PressButton(bool isPressedAlready = false) {
        Transform buttonTransform = ButtonCollider.transform;
        System.Action callback = PressCallback;
        
        buttonTransform.DOKill();

        ButtonCollider.transform.DOLocalMove(isPressedAlready ? _unpressedPosition : _pressedPosition, _pressAnimationDuration)
            .OnComplete(() => { 
                buttonTransform.DOKill();
                if (callback != null) {
                    callback();
                }
            });
    }
}