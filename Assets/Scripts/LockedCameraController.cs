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
        AssingActionToButton(_cameraControlButtons.ZoomIn, null);
    }

    private void AssingActionToButton(Button button, System.Action action) {
       
    }
}