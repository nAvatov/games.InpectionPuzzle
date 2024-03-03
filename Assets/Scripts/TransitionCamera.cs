using System;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;

public class TransitionCamera : ITransitionCamera {
    private CompositeDisposable _disposables;
    private Camera _transitionCamera;
    private List<ClickableCameraView> _clickableViews;
    public Camera CameraReference => _transitionCamera;

    [Inject]
    public void Construct(CompositeDisposable disposables, Camera transitionCameraReference, List<ClickableCameraView> clickableViews) {
        _transitionCamera = transitionCameraReference;
        _clickableViews = clickableViews;

        Debug.Log(_clickableViews.Count);

        AssingClickableViews();
    }

    private void AssingClickableViews() {
        foreach(ClickableCameraView clickableView in _clickableViews) {
            clickableView.ClickableCollider.OnMouseDownAsObservable()
                .Subscribe(_ => {
                    MoveToCamera(clickableView.TargetCamera);
                })
                .AddTo(_disposables);
        }
    }

    public void MoveToCamera(Camera cam) {
        _transitionCamera.transform.DOMove(cam.transform.position, 2f);
        _transitionCamera.transform.DORotate(cam.transform.rotation.eulerAngles, 2f);
    }

    public void ResetPosition() {
        throw new NotImplementedException();
    }
}