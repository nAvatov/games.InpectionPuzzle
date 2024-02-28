using UniRx;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller {
    private CompositeDisposable _disposables = new CompositeDisposable();

    [Header("Hover notifications")]
    [SerializeField] private UI_MainCommonCanvas _mainCommonCanvas;
    [SerializeField] private UI_Notifier _notifier;
    [SerializeField] private HoverConfigurationStruct _hoverConfiguration;
    
    public override void InstallBindings() {
        Container.Bind<CompositeDisposable>().FromInstance(_disposables);

        BindNotificationService();
    }

    private void BindNotificationService() {
        Container.Bind<IMainCommonCanvas>().To<UI_MainCommonCanvas>().FromInstance(_mainCommonCanvas);
        Container.BindInstance(_notifier).AsSingle();
        Container.BindInstance(_hoverConfiguration);
    }

    private void OnDestroy() {
        _disposables.Dispose();
    }
}