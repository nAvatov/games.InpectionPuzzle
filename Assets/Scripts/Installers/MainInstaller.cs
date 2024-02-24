using UniRx;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller {
    private CompositeDisposable _disposables = new CompositeDisposable();
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CameraControlButtonsStruct _cameraControlButtons;
    [SerializeField] private FanPartsStruct _fanParts;
    public override void InstallBindings() {
        Container.Bind<CompositeDisposable>().FromInstance(_disposables);

        BindFanDependencies();
        BindCameraService();
    }

    private void BindCameraService() {
        Container.BindInstance(_cameraControlButtons);
        Container.BindInstance(_mainCamera);
        Container.Bind<ILockedCamera>().To<LockedCamera>().AsSingle();
        Container.Bind(typeof(IInitializable)).To<LockedCameraController>().AsSingle().NonLazy();
    }

    private void BindFanDependencies() {
        Container.BindInstance(_fanParts);
        
        Container.BindInterfacesAndSelfTo<Fan>().AsSingle().NonLazy();
    }

    private void OnDestroy() {
        _disposables.Dispose();
    }
}