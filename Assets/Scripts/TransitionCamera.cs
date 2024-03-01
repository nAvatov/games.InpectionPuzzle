using System;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System.Collections.Generic;

public class TransitionCamera : ITransitionCamera {
    private Camera _transitionCamera;
    private List<BoxCollider> _transitionColliders;
    public Camera CameraReference => _transitionCamera;

    [Inject]
    public void Construct(Camera transitionCameraReference, List<BoxCollider> transitionColliders) {
        _transitionCamera = transitionCameraReference;
        _transitionColliders = transitionColliders;
    }

    

    public void MoveToCamera(Camera cam) {
        _transitionCamera.transform.DOMove(cam.transform.position, 2f);
        _transitionCamera.transform.DORotate(cam.transform.rotation.eulerAngles, 2f);
    }

    public void ResetPosition() {
        throw new NotImplementedException();
    }
}