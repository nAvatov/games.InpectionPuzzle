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
    private ClickableCameraView _currentSelectedCameraView;

    public ClickableCameraView CurrentCameraView {
        set {
            if (_currentSelectedCameraView != null) {
                _currentSelectedCameraView.ClickableCollider.enabled = true;
            }

            _currentSelectedCameraView = value;

            _currentSelectedCameraView.ClickableCollider.enabled = false;
        }

        get => _currentSelectedCameraView;
    }
    public Camera CameraReference => _transitionCamera;

    [Inject]
    public void Construct(CompositeDisposable disposables, Camera transitionCameraReference, List<ClickableCameraView> clickableViews) {
        _disposables = disposables;
        _transitionCamera = transitionCameraReference;
        _clickableViews = clickableViews;

        AssingClickableViews();
    }

    private void AssingClickableViews() {
        foreach(ClickableCameraView clickableView in _clickableViews) {
            clickableView.ClickableCollider.OnMouseDownAsObservable()
                .Subscribe(_ => {
                    CurrentCameraView = clickableView;
                    MoveToCamera(clickableView.TargetCameraTransform, clickableView.OnInspectionStartHandler);
                })
                .AddTo(_disposables);
        }
    }

    public void MoveToCamera(Transform targetTransform, System.Action ClickableViewCallback) {
        _transitionCamera.transform.DORotate(targetTransform.rotation.eulerAngles, 2f);
        _transitionCamera.transform.DOMove(targetTransform.position, 2f)
            .OnComplete(() => {
                _transitionCamera.enabled = false;
                ClickableViewCallback();
                _transitionCamera.DOKill();
            });
    }

    public void ResetPosition() {
        throw new NotImplementedException();
    }
}