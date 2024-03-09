using System;
using UnityEngine;
using Zenject;

public class ClickableCameraView: MonoBehaviour {
    [SerializeField] private Camera _camera;    
    [SerializeField] private BoxCollider _collider;
    [Inject] private LockedCameraController _cameraController;

    public Camera TargetCamera => _camera;
    public BoxCollider ClickableCollider => _collider;

    public void OnInspectionStartHandler() {
        _cameraController.EnableMouseGestures();
    }
}