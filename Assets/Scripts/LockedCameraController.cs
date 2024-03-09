using UniRx;
using UnityEngine;
using Zenject;

public class LockedCameraController {
    private ILockedCamera _lockedCamera;
    private CameraControlButtonsStruct _cameraControlButtons;
    private CompositeDisposable _disposables;
    private Vector2 _mousePressPoint;
    private SerialDisposable _mousePressPointObservable;
    private SerialDisposable _mouseDeltaPointObservable;

    [Inject] 
    public void Construct(ILockedCamera cam, CameraControlButtonsStruct cameraControlButtons, CompositeDisposable disposables) {
        _lockedCamera = cam;
        _cameraControlButtons = cameraControlButtons;

        _disposables = disposables;

        InitializeCameraControls();
    }

    private void InitializeCameraControls() {
        AssingActionToButton(_cameraControlButtons.ZoomIn, () => _lockedCamera.ChangeDistanceToObject(Vector3.forward));
        AssingActionToButton(_cameraControlButtons.ZoomOut, () => _lockedCamera.ChangeDistanceToObject(Vector3.back));
        AssingActionToButton(_cameraControlButtons.MoveAroundLeft, () => _lockedCamera.MoveAroundObject(Vector3.up));
        AssingActionToButton(_cameraControlButtons.MoveAroundRight, () => _lockedCamera.MoveAroundObject(Vector3.down));

        AssingCameraRestore();
    }

    public void EnableMouseGestures() {
        _mousePressPointObservable = new SerialDisposable().AddTo(_disposables);
        _mouseDeltaPointObservable = new SerialDisposable().AddTo(_disposables);

        _mousePressPointObservable.Disposable = Observable
            .EveryUpdate()
            .Where(_ => {
                return Input.GetMouseButtonDown(0);
            })
            .Subscribe(_ => {
                _mousePressPoint = Input.mousePosition;
            })
            .AddTo(_disposables);
        
        _mouseDeltaPointObservable.Disposable = Observable
            .EveryUpdate()
            .Where(_ => {
                return Input.GetMouseButton(0);
            })
            .Subscribe(_ => {
                _lockedCamera.RotateCameraByGesture(_mousePressPoint, new Vector2(Input.mousePosition.x, Input.mousePosition.y));

                _mousePressPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y); 
            })
            .AddTo(_disposables);

        Debug.Log("Camera gestures enabled!");
    }

    public void DisableMouseGestures() {
        _mousePressPointObservable.Disposable.Dispose();
        _mousePressPointObservable.Disposable = null;

        _mouseDeltaPointObservable.Disposable.Dispose();
        _mouseDeltaPointObservable.Disposable = null;

        Debug.Log("Camera gestures disabled!");
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
        Observable.EveryUpdate()
            .Where(_ => button.IsDown.Value)
            .Subscribe(_ => action())
            .AddTo(_disposables);
    }

    private void AssingCameraRestore() {
        _cameraControlButtons.RestorePosition.OnClickAsObservable()
            .Subscribe(_ => {
                _lockedCamera.RestoreTransform();
            })
            .AddTo(_disposables);
    }
}