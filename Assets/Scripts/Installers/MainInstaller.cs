using UniRx;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller {
    private CompositeDisposable _disposables = new CompositeDisposable();
    [SerializeField] private UI_Notificator _notificator;
    [SerializeField] private HoverConfigurationStruct _hoverConfiguration;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CameraControlButtonsStruct _cameraControlButtons;
    [SerializeField] private GameObjectContext _fanContext;
    public override void InstallBindings() {
        _fanContext.Install(Container);
        
        Container.Bind<CompositeDisposable>().FromInstance(_disposables);

        Container.BindInstance(_fanContext.Container.Resolve<ITargetObject>());
        Container.BindInstance(_hoverConfiguration);

        BindCameraService();
        BindNotificationService();
    }

    private void BindCameraService() {
        Container.BindInstance(_cameraControlButtons);
        Container.BindInstance(_mainCamera);
        Container.Bind<ILockedCamera>().To<LockedCamera>().AsSingle();
        Container.Bind(typeof(IInitializable)).To<LockedCameraController>().AsSingle().NonLazy();
    }

    private void BindNotificationService() {
        Container.BindInstance(_notificator).AsSingle();
    }

    private void OnDestroy() {
        _disposables.Dispose();
    }
}