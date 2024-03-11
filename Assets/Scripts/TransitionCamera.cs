using System;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;

public class TransitionCamera : ITransitionCamera {
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;

    private CompositeDisposable _disposables;
    private Camera _transitionCamera;
    private CommonInspectionUiStruct _commonInspectionUI;
    private List<ClickableCameraView> _clickableViews;
    private ClickableCameraView _currentSelectedCameraView;

    public ClickableCameraView CurrentCameraView {
        set {
            if (_currentSelectedCameraView != null) {
                if (value == null) {
                    _currentSelectedCameraView.OnInspectionEndHandler();
                    _currentSelectedCameraView.ClickableCollider.enabled = true;
                    _currentSelectedCameraView = value;
                    return;
                }

                _currentSelectedCameraView.ClickableCollider.enabled = true;
            }

            _currentSelectedCameraView = value;
            _currentSelectedCameraView.ClickableCollider.enabled = false;
        }

        get => _currentSelectedCameraView;
    }
    public Camera CameraReference => _transitionCamera;

    [Inject]
    public void Construct(CompositeDisposable disposables, Camera transitionCameraReference, CommonInspectionUiStruct commonInspectionUI, List<ClickableCameraView> clickableViews) {
        _initialPosition = transitionCameraReference.transform.localPosition;
        _initialRotation = transitionCameraReference.transform.localRotation.eulerAngles;
        
        _disposables = disposables;
        _transitionCamera = transitionCameraReference;
        _commonInspectionUI = commonInspectionUI;
        _clickableViews = clickableViews;

        AssingPositionReset();
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

    private void AssingPositionReset() {
        _commonInspectionUI._stopInspectionButton.OnClickAsObservable()
            .Subscribe(_ => ResetPosition())
            .AddTo(_disposables);
    }

    public void MoveToCamera(Transform targetTransform, System.Action ClickableViewCallback) {
        _transitionCamera.transform.DOKill();

        _transitionCamera.transform.DORotate(targetTransform.rotation.eulerAngles, 2f);
        _transitionCamera.transform.DOMove(targetTransform.position, 2f)
            .OnComplete(() => {
                _transitionCamera.enabled = false;
                _commonInspectionUI.FadeInInspectionUI();

                ClickableViewCallback();
                
                _transitionCamera.transform.DOKill();
            });
    }

    public void ResetPosition() {
        _transitionCamera.enabled = true;
        CurrentCameraView = null;

        _commonInspectionUI.FadeOutInspectionUI();

        _transitionCamera.transform.DORotate(_initialRotation, 2f);
        _transitionCamera.transform.DOMove(_initialPosition, 2f)
            .OnComplete(() => {
                _transitionCamera.transform.DOKill();
            });
    }
}