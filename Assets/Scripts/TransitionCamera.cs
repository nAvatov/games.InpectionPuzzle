using System;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class TransitionCamera : ITransitionCamera {
    private Camera _transitionCamera;
    public Camera CameraReference => _transitionCamera;

    [Inject]
    public void Construct(Camera transitionCameraReference) {
        _transitionCamera = transitionCameraReference;
    }

    public void MoveToCamera(Camera cam) {
        _transitionCamera.transform.DOMove(cam.transform.position, 2f);
        _transitionCamera.transform.DORotate(cam.transform.rotation.eulerAngles, 2f);
    }

    public void ResetPosition() {
        throw new NotImplementedException();
    }
}