using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using DG.Tweening;
using System;

public class Fan : IFanInteractive, ITargetObject {
    private CompositeDisposable _disposables;
    private bool _isOn = false;
    private bool _isBodyRotating = false;
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
        SignHingeStream();
    }

    private void SignSwitchStream() {
        _fanParts.SwitchButton.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!_isOn) {
                    StartBladesRotating();
                } else {
                    StopBladesRotating();
                }

                _isOn = !_isOn;
            })
            .AddTo(_disposables);
    }

    private void SignBodyStream() {
        _fanParts.BodyRotationButton.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!_isBodyRotating) {
                    StartBodyRotation();
                } else {
                    StopBodyRotation();
                }

                _isBodyRotating = !_isBodyRotating;
            })
            .AddTo(_disposables);
    }

    private void SignHingeStream() {
        _fanParts.HingeHandle.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!_isHingeTilting) {
                    StartHingeTilting();
                } else {
                    StopHingeTilting();
                }

                _isHingeTilting = !_isHingeTilting;
            })
            .AddTo(_disposables);
    }

    private void StopHingeTilting() {
        _fanParts.HingeHandle.enabled = false;

        _fanParts.Hinge.DORotate(new Vector3(0, 0, 0), 1f)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                _fanParts.Hinge.DOKill();
                _fanParts.HingeHandle.enabled = true;
            });
    }

    private void StartHingeTilting() {
        _fanParts.Hinge.DORotate(new Vector3(15f, 0, 0), 3f)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _fanParts.Hinge.DORotate(new Vector3(-45f, 0, 0), 3f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => StartHingeTilting());
            });

    }

    private void StartBladesRotating() {
        _fanParts.SwitchButton.enabled = false;

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

    private void StopBladesRotating() {
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
        _fanParts.Body
            .DOLocalRotate(new Vector3(0, -45f, 0), 3f)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                Debug.Log("On Complete body");
                _fanParts.Body
                    .DOLocalRotate(new Vector3(0, 45f, 0), 3f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => StartBodyRotation());
            });
    }

    private void StopBodyRotation() {
        _fanParts.BodyRotationButton.enabled = false;

        _fanParts.Body
            .DOLocalRotate(new Vector3(0, 0, 0), 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _fanParts.Body.DOKill();
                _fanParts.BodyRotationButton.enabled = true;
            });
    }
}
