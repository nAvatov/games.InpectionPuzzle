using DG.Tweening;
using UnityEngine;
using Zenject;

public class LockedCamera : ILockedCamera {
    private LockedCameraConfiguration _configuration;
    private Transform _targetTransform;
    private Transform _cameraTransform;
    private Vector3 _rotationVectorByGesture;
    private Vector2 _gesturePosDelta;
    
    [Inject]
    public void Construct(Camera cam, ITargetObject targetObject, LockedCameraConfiguration configuration) {
        _configuration = configuration;
        _targetTransform = targetObject.targetTransform;
        _cameraTransform = cam.transform;
    }    

    public void ChangeDistanceToObject(Vector3 direction) {
        if (direction == Vector3.forward && _cameraTransform.localPosition.z > _configuration.ZoomInRestriction) {
            _cameraTransform.Translate(direction * _configuration.CameraMoveSpeedByButtons * Time.deltaTime);
            return;
        }

        if (direction == Vector3.back && _cameraTransform.localPosition.z < _configuration.ZoomOutRestriction) {
            _cameraTransform.Translate(direction * _configuration.CameraMoveSpeedByButtons * Time.deltaTime);
            return;
        }
    }

    public void MoveAroundObject(Vector3 direction) {
        _cameraTransform.RotateAround(_targetTransform.position, direction, _configuration.CameraMoveSpeedByButtons);
    }

    public void RotateCameraByGesture(Vector2 gestureStartPos, Vector2 gestureCurrentPos) {
        _gesturePosDelta = gestureCurrentPos - gestureStartPos;

        _rotationVectorByGesture.x = -_gesturePosDelta.y;
        _rotationVectorByGesture.y = _gesturePosDelta.x;
        _rotationVectorByGesture.z = 0;

        _cameraTransform.RotateAround(_targetTransform.position, _rotationVectorByGesture.normalized, _configuration.CameraMoveSpeedByGestures);
        _cameraTransform.rotation = Quaternion.Euler(_cameraTransform.rotation.eulerAngles.x, _cameraTransform.rotation.eulerAngles.y, 0f);
    }

    public void RestoreTransform() {
        _cameraTransform.DOLocalRotate(_configuration.InitialRotation, _configuration.RestoreDuration);
        _cameraTransform.DOLocalMove(_configuration.InitialPosition, _configuration.RestoreDuration)
            .OnComplete(() => _cameraTransform.DOKill());
    }
}