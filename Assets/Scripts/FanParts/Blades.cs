using UnityEngine;
using DG.Tweening;
using Zenject;

public class Blades : IRotatableFanPart
{
    private bool _isRotating;
    private Transform _blades;
    private PressableButtonStruct _pressableButton;
    [Inject] private BladesConfigurationStruct _configuration;
    [Inject] private FanAudioSourcesStruct _fanAudioSources;
    [Inject] private FanSoundsConfiguration _soundConfiguration;
    
    public bool IsRotating { 
        get => _isRotating; 
        set {
            _isRotating = value;
        }
    }
    public Transform Part => _blades;
    public Collider Button => _pressableButton.ButtonCollider;

    public void Construct(Transform blades, PressableButtonStruct pressableButton) {
        _blades = blades;
        _pressableButton = pressableButton;
    }

    public void StartRotation() {
        _pressableButton.ButtonCollider.enabled = false;
        _blades.DOKill();

        PlayFanNoise();
        _pressableButton.PressButton();

        Vector3 rotationVector = new Vector3(0, 0, _configuration.RollAngle);

        _blades
            .DOLocalRotate(rotationVector, _configuration.StartingRollDuration, RotateMode.LocalAxisAdd)
            .SetRelative(true)
            .SetEase(Ease.InSine)
            .OnComplete(() => {
                _pressableButton.ButtonCollider.enabled = true;
                _blades.DOLocalRotate(rotationVector, _configuration.RollDuration, RotateMode.LocalAxisAdd)
                    .SetRelative(true)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1);
            });
    }

    public void StopRotation() {
        _pressableButton.ButtonCollider.enabled = false;
        
        StopPlayingFanNoise();
        _pressableButton.PressButton(true);

        _blades
            .DORotate(new Vector3(0, 0, _configuration.RollAngle), _configuration.StopRollDuration)
            .SetRelative(true)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                _blades.DOKill();
                _pressableButton.ButtonCollider.enabled = true;
            });
    }

    private void PlayFanNoise() {
        _fanAudioSources.BladesAudioSource.pitch = _configuration.LowPitchValue;
        _fanAudioSources.BladesAudioSource.clip = _soundConfiguration.FanNoise;
        _fanAudioSources.BladesAudioSource.Play();
        _fanAudioSources.BladesAudioSource.DOPitch(1f, _configuration.StartingRollDuration);
        _fanAudioSources.BladesAudioSource.loop = true;
    }

    private void StopPlayingFanNoise() {
        _fanAudioSources.BladesAudioSource.DOPitch(_configuration.LowPitchValue, _configuration.StopRollDuration)
            .OnComplete(() => {
                _fanAudioSources.BladesAudioSource.Stop();
                _fanAudioSources.BladesAudioSource.loop = false;
                _fanAudioSources.BladesAudioSource.pitch = 1f;
                _fanAudioSources.BladesAudioSource.DOKill();
            });
    }
}