using UniRx;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller {
    private CompositeDisposable _disposables = new CompositeDisposable();
    [SerializeField] private CameraControlButtons _cameraControlButtons;
    public override void InstallBindings() {
        Container.Bind<CompositeDisposable>().FromInstance(_disposables);

        Container.Bind<ILockedCamera>().To<LockedCamera>().AsSingle();
        Container.BindInstance(_cameraControlButtons);

        Container.Bind(typeof(IInitializable)).To<LockedCameraController>().AsSingle().NonLazy();
    }

    private void OnDisable() {
        _disposables.Dispose();
    }
}