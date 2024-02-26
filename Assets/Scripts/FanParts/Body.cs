using UnityEngine;
using DG.Tweening;
using Zenject;

public class Body : IRotatableFanPart {
    private bool _isRotating;
    private Transform _body;
    private Collider _button;
    [Inject] private BodyConfigurationStruct _configuration;
    
    public bool IsRotating { 
        get => _isRotating; 
        set {
            _isRotating = value;
        }
    }
    public Transform Part => _body;
    public Collider Button => _button;

    public void Construct(Transform body, Collider button) {
        _body = body;
        _button = button;
    }

    public void StartRotation() {
        _body
            .DOLocalRotate(new Vector3(0, -_configuration.YawAngle, 0), _configuration.YawDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _body
                    .DOLocalRotate(new Vector3(0, _configuration.YawAngle, 0), _configuration.YawDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => StartRotation());
            });
    }

    public void StopRotation() {
        _button.enabled = false;

        _body
            .DOLocalRotate(new Vector3(0, 0, 0), _configuration.StopYawDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _body.DOKill();
                _button.enabled = true;
            });
    }
}