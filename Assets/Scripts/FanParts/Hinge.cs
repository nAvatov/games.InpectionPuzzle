using UnityEngine;
using DG.Tweening;
using Zenject;

public class Hinge : IRotatableFanPart {
    private bool _isRotating;
    private Transform _hinge;
    private PressableButtonStruct _pressableButton;
    [Inject] private HingeConfigurationStruct _configuration;
    
    public bool IsRotating { 
        get => _isRotating; 
        set {
            _isRotating = value;
        }
    }
    public Transform Part => _hinge;
    public Collider Button => _pressableButton.ButtonCollider;

    public void Construct(Transform hinge, PressableButtonStruct pressableButton) {
        _hinge = hinge;
        _pressableButton = pressableButton;

        _pressableButton.PressCallback = () => _pressableButton.ButtonCollider.enabled = true;
    }

    public void StartRotation() {
        _pressableButton.ButtonCollider.enabled = false;

        _hinge.DOKill();
        HingePinchingRoutine();
    }

    private void HingePinchingRoutine() {
        _hinge.DOLocalRotate(new Vector3(_configuration.PitchDownAngle, 0, 0), _configuration.TiltDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _hinge.DOLocalRotate(new Vector3(_configuration.PitchUpAngle, 0, 0), _configuration.TiltDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => HingePinchingRoutine());
            });
    }

    public void StopRotation() {
        _pressableButton.ButtonCollider.enabled = false;

        _hinge.DOLocalRotate(new Vector3(0, 0, 0), _configuration.StopTiltDuration)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                _hinge.DOKill();
                _pressableButton.ButtonCollider.enabled = true;
            });
    }
}