using System;
using UnityEngine;

public class ClickableCameraView: MonoBehaviour {
    [SerializeField] private Camera _camera;    
    [SerializeField] private BoxCollider _collider;

    public Camera TargetCamera => _camera;
    public BoxCollider ClickableCollider => _collider;
}