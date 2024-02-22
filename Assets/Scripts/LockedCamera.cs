using System;
using UnityEngine;
using Zenject;

public class LockedCamera : ILockedCamera {
    private const float _cameraMoveSpeed = 0.5f;
    private Transform _targetTransform;
    private Transform _cameraTransform;
    private Vector3 _zoomInRestriction;
    
    [Inject]
    public void Construct(Camera cam, ITargetObject targetObject) {
        _targetTransform = targetObject.targetTransform;
        _cameraTransform = cam.transform;
        _zoomInRestriction = cam.transform.position;
    }    

    public void ChangeDistanceToObject(Vector3 direction) {
        if (direction == Vector3.forward && Vector3.Distance(_cameraTransform.position, _zoomInRestriction) > 0) {
            _cameraTransform.Translate(direction * _cameraMoveSpeed * Time.deltaTime, Space.World);
            _cameraTransform.LookAt(_targetTransform);
        } else {
            _cameraTransform.Translate(direction * _cameraMoveSpeed * Time.deltaTime, Space.World);
            _cameraTransform.LookAt(_targetTransform);
        }
    }

    public void MoveAroundObject(Vector3 direction) {
        //_cameraTransform.Translate(direction * _cameraMoveSpeed * Time.deltaTime, Space.World);
        _cameraTransform.RotateAround(_targetTransform.position, direction, _cameraMoveSpeed);
    }
}