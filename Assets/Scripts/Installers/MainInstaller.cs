using UniRx;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller {
    private CompositeDisposable _disposables = new CompositeDisposable();
    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;

    [Header("Hover notifications")]
    [SerializeField] private UI_Notifier _notifier;
    [SerializeField] private HoverConfigurationStruct _hoverConfiguration;
    
    public override void InstallBindings() {
        Container.Bind<CompositeDisposable>().FromInstance(_disposables);

        BindNotificationService();
        Container.BindInstance(_audioSource);
    }

    private void BindNotificationService() {
        Container.BindInstance(_notifier).AsSingle();
        Container.BindInstance(_hoverConfiguration);
    }

    private void OnDestroy() {
        _disposables.Dispose();
    }
}