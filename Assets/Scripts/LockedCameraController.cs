using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LockedCameraController: IInitializable {
    private ILockedCamera _lockedCamera;
    private CameraControlButtonsStruct _cameraControlButtons;
    private CompositeDisposable _disposables;
    private Vector2 _mousePressPoint;
    private Vector2 _mousePressPointDelta;

    [Inject] 
    public void Construct(ILockedCamera cam, CameraControlButtonsStruct cameraControlButtons, CompositeDisposable disposables) {
        _lockedCamera = cam;
        _cameraControlButtons = cameraControlButtons;

        _disposables = disposables;
    }

    public void Initialize() {
        AssingActionToButton(_cameraControlButtons.ZoomIn, () => _lockedCamera.ChangeDistanceToObject(Vector3.forward));
        AssingActionToButton(_cameraControlButtons.ZoomOut, () => _lockedCamera.ChangeDistanceToObject(Vector3.back));
        AssingActionToButton(_cameraControlButtons.MoveAroundLeft, () => _lockedCamera.MoveAroundObject(Vector3.down));
        AssingActionToButton(_cameraControlButtons.MoveAroundRight, () => _lockedCamera.MoveAroundObject(Vector3.up));

        AssingMouseGestures();
    }

    private void AssingMouseGestures() {
        Observable
            .EveryUpdate()
            .Where(_ => {
                return Input.GetMouseButtonDown(0);
            })
            .Subscribe(_ => {
                _mousePressPoint = Input.mousePosition;
            })
            .AddTo(_disposables);

        Observable
            .EveryUpdate()
            .Where(_ => {
                return Input.GetMouseButton(0);
            })
            .Subscribe(_ => {
                _lockedCamera.RotateCameraByGesture(_mousePressPoint, new Vector2(Input.mousePosition.x, Input.mousePosition.y));

                _mousePressPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y); 
            })
            .AddTo(_disposables);
    }

    private void AssingActionToButton(UIButtonAdditional button, System.Action action) {
        Observable
            .EveryUpdate()
            .Where(_ => {
                return button.IsDown;
            })
            .Subscribe(_ => {
                action();
            })
            .AddTo(_disposables);
    }
}