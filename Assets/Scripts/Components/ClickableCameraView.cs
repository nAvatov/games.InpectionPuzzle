using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClickableCameraView: MonoBehaviour {
    [SerializeField] private GraphicRaycaster _canvasGraphicRaycaster;
    [SerializeField] private Camera _camera;    
    [SerializeField] private BoxCollider _collider;
    [Inject] private LockedCameraController _cameraController;
    [Inject] private ILockedCamera _lockedCamera;

    public Transform TargetCameraTransform => _camera.transform;
    public BoxCollider ClickableCollider => _collider;

    public void OnInspectionStartHandler() {
        _canvasGraphicRaycaster.enabled = true;
        _camera.enabled = true;
        _cameraController.EnableMouseGestures();
    }

    public void OnInspectionEndHandler() {
        _canvasGraphicRaycaster.enabled = false;
        _camera.enabled = false;
        _cameraController.DisableMouseGestures();
        _lockedCamera.RestoreTransform();
    }
}