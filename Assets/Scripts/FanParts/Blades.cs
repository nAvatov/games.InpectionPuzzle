using UnityEngine;
using DG.Tweening;
using Zenject;

public class Blades : IRotatableFanPart
{
    private bool _isRotating;
    private Transform _blades;
    private Collider _button;
    [Inject] private BladesConfigurationStruct _configuration;
    
    public bool IsRotating { 
        get => _isRotating; 
        set {
            _isRotating = value;
        }
    }
    public Transform Part => _blades;
    public Collider Button => _button;

    public void Construct(Transform blades, Collider button) {
        _blades = blades;
        _button = button;
    }

    public void StartRotation() {
       _button.enabled = false;

        Vector3 rotationVector = new Vector3(0, 0, _configuration.RollAngle);

        _blades
            .DOLocalRotate(rotationVector, _configuration.StartingRollDuration, RotateMode.LocalAxisAdd)
            .SetRelative(true)
            .SetEase(Ease.InSine)
            .OnComplete(() => {
                _button.enabled = true;
                _blades.DOLocalRotate(rotationVector, _configuration.RollDuration, RotateMode.LocalAxisAdd)
                    .SetRelative(true)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1);
            });
    }

    public void StopRotation() {
        _button.enabled = false;
        
        _blades
            .DORotate(new Vector3(0, 0, _configuration.RollAngle), _configuration.StopRollDuration)
            .SetRelative(true)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                _blades.DOKill();
                _button.enabled = true;
            });
    }
}