using System;
using UnityEngine;
using Zenject;

public class LockedCamera : ILockedCamera {
    private const float _cameraMoveSpeedButtons = 0.5f;
    private const float _cameraMoveSpeedGestures = 1f;
    private Transform _targetTransform;
    private Transform _cameraTransform;
    private Vector3 _zoomInRestriction;
    private Vector3 _rotationVectorByGesture;
    private Vector2 _gesturePosDelta;
    
    [Inject]
    public void Construct(Camera cam, ITargetObject targetObject) {
        _targetTransform = targetObject.targetTransform;
        _cameraTransform = cam.transform;
        _zoomInRestriction = cam.transform.position;
    }    

    public void ChangeDistanceToObject(Vector3 direction) {
        if (direction == Vector3.forward && Vector3.Distance(_cameraTransform.position, _zoomInRestriction) > 0) {
            _cameraTransform.Translate(direction * _cameraMoveSpeedButtons * Time.deltaTime, Space.World);
        } else {
            _cameraTransform.Translate(direction * _cameraMoveSpeedButtons * Time.deltaTime, Space.World);
        }
    }

    public void MoveAroundObject(Vector3 direction) {
        _cameraTransform.RotateAround(_targetTransform.position, direction, _cameraMoveSpeedButtons);
    }

    public void RotateCameraByGesture(Vector2 gestureStartPos, Vector2 gestureCurrentPos) {
        _gesturePosDelta = gestureCurrentPos - gestureStartPos;

        _rotationVectorByGesture.x = -_gesturePosDelta.y;
        _rotationVectorByGesture.y = _gesturePosDelta.x;
        _rotationVectorByGesture.z = 0;

        _cameraTransform.RotateAround(_targetTransform.position, _rotationVectorByGesture, _cameraMoveSpeedGestures);
    }
}