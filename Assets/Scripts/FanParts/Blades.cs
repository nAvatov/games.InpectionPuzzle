using UnityEngine;
using DG.Tweening;
using Zenject;

public class Blades : IRotatableFanPart
{
    private bool _isRotating;
    private Transform _blades;
    private Collider _button;
    [Inject] private BladesConfigurationStruct _configuration;
    [Inject] private AudioSource _fanAudioSource;
    [Inject] private SoundConfigurationStruct _soundConfiguration;
    
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
        PlayFanNoise();

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
        
        StopPlayingFanNoise();

        _blades
            .DORotate(new Vector3(0, 0, _configuration.RollAngle), _configuration.StopRollDuration)
            .SetRelative(true)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                _blades.DOKill();
                _button.enabled = true;
            });
    }

    private void PlayFanNoise() {
        _fanAudioSource.pitch = _configuration.LowPitchValue;
        _fanAudioSource.clip = _soundConfiguration.FanNoise;
        _fanAudioSource.Play();
        _fanAudioSource.DOPitch(1f, _configuration.StartingRollDuration);
        _fanAudioSource.loop = true;
    }

    private void StopPlayingFanNoise() {
        _fanAudioSource.DOPitch(_configuration.LowPitchValue, _configuration.StopRollDuration)
            .OnComplete(() => {
                _fanAudioSource.Stop();
                _fanAudioSource.loop = false;
                _fanAudioSource.pitch = 1f;
                _fanAudioSource.DOKill();
            });
    }
}