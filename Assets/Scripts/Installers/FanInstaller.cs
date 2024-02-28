using UnityEngine;
using Zenject;

public class FanInstaller : MonoInstaller {
    [SerializeField] private AudioSource _fanAudioSource;
    [Header("Attached Camera Dependencies")]
    [SerializeField] private Camera _attacheCamera;
    [SerializeField] private CameraControlButtonsStruct _cameraControlButtons;
    [SerializeField] private LockedCameraConfiguration _cameraConfiguration;

    [Header("Fan Dependencies")]
    [SerializeField] private FanPartsStruct _fanParts;
    [SerializeField] private HingeConfigurationStruct _hingeConfiguration;
    [SerializeField] private BladesConfigurationStruct _bladesConfiguration;
    [SerializeField] private BodyConfigurationStruct _bodyConfiguration;
    [SerializeField] private SoundConfigurationStruct _soundConfiguration;
    public override void InstallBindings() {
        Container.BindInstance(_fanAudioSource);
        BindFanDependencies();
        BindAttachedCameraDependencies();
    }

    private void BindFanDependencies() {
        Container.BindInstance(_soundConfiguration);
        Container.BindInstance(_fanParts);

        Container.BindInstance(_bladesConfiguration);
        Container.BindInstance(_bodyConfiguration);
        Container.BindInstance(_hingeConfiguration);

        Container.Bind<Blades>().AsSingle();
        Container.Bind<Body>().AsSingle();
        Container.Bind<Hinge>().AsSingle();

        Container.BindInterfacesAndSelfTo<FanController>().AsSingle().NonLazy();
    }

    private void BindAttachedCameraDependencies() {
        Container.BindInstance(_cameraConfiguration);
        Container.BindInstance(_cameraControlButtons);
        Container.BindInstance(_attacheCamera);
        Container.Bind<ILockedCamera>().To<LockedCamera>().AsSingle();
        Container.Bind(typeof(IInitializable)).To<LockedCameraController>().AsSingle().NonLazy();
    }
}