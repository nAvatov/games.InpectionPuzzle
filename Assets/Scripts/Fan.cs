using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class Fan : IFanInteractive, ITargetObject {
    private CompositeDisposable _disposables;
    private bool _isOn = false;
    private bool _isHingeTilting = false;
    private FanPartsStruct _fanParts;

    public Transform targetTransform => _fanParts.ViewCenter;
    public FanPartsStruct FanParts => _fanParts;

    [Inject]
    public void Construct(CompositeDisposable disposables, FanPartsStruct parts) {
        _disposables = disposables;
        
        _fanParts = parts;

        SignSwitchStream();
        SignBodyStream();
    }

    private void SignSwitchStream() {
        _fanParts.SwitchButton.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!_isOn) {
                    SwitchOnAnimation();
                } else {
                    SwitchOffAnimation();
                }

                _isOn = !_isOn;
            })
            .AddTo(_disposables);
    }

    private void SignBodyStream() {
        _fanParts.BodyRotationButton.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!_isHingeTilting) {
                    StartBodyRotation();
                } else {

                }
            })
            .AddTo(_disposables);
    }

    private void SwitchOnAnimation() {
        _fanParts.SwitchButton.enabled = false;

        Vector3 v = Quaternion.AngleAxis(360f, Vector3.forward).eulerAngles;

        Vector3 rotationVector = new Vector3(0, 0, 360f);

        _fanParts.FanBlades
            .DOLocalRotate(rotationVector, 1f, RotateMode.LocalAxisAdd)
            .SetRelative(true)
            .SetEase(Ease.InSine)
            .OnComplete(() => {
                _fanParts.SwitchButton.enabled = true;
                _fanParts.FanBlades.DOLocalRotate(rotationVector, 0.5f, RotateMode.LocalAxisAdd)
                    .SetRelative(true)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1);
            });
    }

    private void SwitchOffAnimation() {
        _fanParts.SwitchButton.enabled = false;
        
        _fanParts.FanBlades
            .DORotate(new Vector3(0, 0, 360f), 1f)
            .SetRelative(true)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                _fanParts.FanBlades.DOKill();
                _fanParts.SwitchButton.enabled = true;
            });
    }

    private void StartBodyRotation() {
        _fanParts.BodyRotationButton.enabled = false;

        _fanParts.Body
            .DORotate(new Vector3(0, 45f, 0), 4f)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _fanParts.Body
                    .DORotate(new Vector3(0, -45f, 0), 4f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => StartBodyRotation());
            });
    }
}
