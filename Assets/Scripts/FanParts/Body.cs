using UnityEngine;
using DG.Tweening;
using Zenject;

public class Body : IRotatableFanPart {
    private bool _isRotating;
    private Transform _body;
    private PressableButtonStruct _pressableButton;
    [Inject] private BodyConfigurationStruct _configuration;
    
    public bool IsRotating { 
        get => _isRotating; 
        set {
            _isRotating = value;
        }
    }
    public Transform Part => _body;
    public Collider Button => _pressableButton.ButtonCollider;

    public void Construct(Transform body, PressableButtonStruct pressableButton) {
        _body = body;
        _pressableButton = pressableButton;
        
        _pressableButton.PressCallback = () => _pressableButton.ButtonCollider.enabled = true;
    }

    public void StartRotation() {
        _pressableButton.ButtonCollider.enabled = false;
        _pressableButton.PressButton();
        
        _body.DOKill();
        BodyYawingRoutin();
    }

    private void BodyYawingRoutin() {
        _body
            .DOLocalRotate(new Vector3(0, -_configuration.YawAngle, 0), _configuration.YawDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _body
                    .DOLocalRotate(new Vector3(0, _configuration.YawAngle, 0), _configuration.YawDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => BodyYawingRoutin());
            });
    }

    public void StopRotation() {
        _pressableButton.ButtonCollider.enabled = false;
        _pressableButton.PressButton(true);

        _body
            .DOLocalRotate(new Vector3(0, 0, 0), _configuration.StopYawDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _body.DOKill();
                _pressableButton.ButtonCollider.enabled = true;
            });
    }
}