using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LockedCameraController: IInitializable {
    private ILockedCamera _lockedCamera;
    private CameraControlButtons _cameraControlButtons;
    private CompositeDisposable _disposables;

    [Inject] 
    public void Construct(ILockedCamera cam, CameraControlButtons cameraControlButtons, CompositeDisposable disposables) {
        _lockedCamera = cam;
        _cameraControlButtons = cameraControlButtons;

        _disposables = disposables;
    }

    public void Initialize() {
        AssingActionToButton(_cameraControlButtons.ZoomIn, () => _lockedCamera.ChangeDistanceToObject(Vector3.forward));
        AssingActionToButton(_cameraControlButtons.ZoomOut, () => _lockedCamera.ChangeDistanceToObject(Vector3.back));
        AssingActionToButton(_cameraControlButtons.MoveAroundLeft, () => _lockedCamera.MoveAroundObject(Vector3.down));
        AssingActionToButton(_cameraControlButtons.MoveAroundRight, () => _lockedCamera.MoveAroundObject(Vector3.up));
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