using UnityEngine;
using Zenject;

public class InspectionItemInstaller : MonoInstaller {
    [Header("Attached Camera Dependencies")]
    [SerializeField] private Camera _attachedCamera;
    [SerializeField] private CameraControlButtonsStruct _cameraControlButtons;
    [SerializeField] private LockedCameraConfiguration _cameraConfiguration;
    public override void InstallBindings() {
        BindAttachedCameraDependencies();
    }

    private void BindAttachedCameraDependencies() {
        Container.BindInstance(_cameraConfiguration);
        Container.BindInstance(_cameraControlButtons);
        Container.BindInstance(_attachedCamera);
        Container.Bind<ILockedCamera>().To<LockedCamera>().AsSingle();
        Container.BindInterfacesAndSelfTo<LockedCameraController>().AsSingle().NonLazy();
    }
}