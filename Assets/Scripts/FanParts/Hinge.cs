using UnityEngine;
using DG.Tweening;
using Zenject;

public class Hinge : IRotatableFanPart {
    private bool _isRotating;
    private Transform _hinge;
    private Collider _button;
    [Inject] private HingeConfigurationStruct _configuration;
    
    public bool IsRotating { 
        get => _isRotating; 
        set {
            _isRotating = value;
        }
    }
    public Transform Part => _hinge;
    public Collider Button => _button;

    public void Construct(Transform hinge, Collider button) {
        _hinge = hinge;
        _button = button;
    }

    public void StartRotation() {
        _hinge.DOLocalRotate(new Vector3(_configuration.PitchDownAngle, 0, 0), _configuration.TiltDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _hinge.DOLocalRotate(new Vector3(_configuration.PitchUpAngle, 0, 0), _configuration.TiltDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => StartRotation());
            });
    }

    public void StopRotation() {
        _button.enabled = false;

        _hinge.DOLocalRotate(new Vector3(0, 0, 0), _configuration.StopTiltDuration)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                _hinge.DOKill();
                _button.enabled = true;
            });
    }
}