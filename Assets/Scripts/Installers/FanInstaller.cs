using UnityEngine;
using Zenject;

public class FanInstaller : MonoInstaller {
    [SerializeField] private FanAudioSourcesStruct _fanAudioSources;
    [Header("Fan Dependencies")]
    [SerializeField] private FanPartsStruct _fanParts;
    [SerializeField] private HingeConfigurationStruct _hingeConfiguration;
    [SerializeField] private BladesConfigurationStruct _bladesConfiguration;
    [SerializeField] private BodyConfigurationStruct _bodyConfiguration;
    [SerializeField] private FanSoundsConfiguration _soundConfiguration;
    public override void InstallBindings() {
        Container.BindInstance(_fanAudioSources);
        BindFanDependencies();
    }

    // Разделить инсталлер на InspectionItem и отдельный инсталлер под конкретный тип предмета.
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
}