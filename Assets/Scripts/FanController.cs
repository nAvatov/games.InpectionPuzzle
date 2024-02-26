using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using DG.Tweening;
using System;

public class FanController : IFanInteractive, ITargetObject {
    private CompositeDisposable _disposables;
    private FanPartsStruct _fanParts;
    private Blades _blades;
    private Body _body;
    private Hinge _hinge;

    public Transform targetTransform => _fanParts.ViewCenter;
    public FanPartsStruct FanParts => _fanParts;

    [Inject]
    public void Construct(CompositeDisposable disposables, FanPartsStruct parts, Blades blades, Body body, Hinge hinge) {
        _disposables = disposables;
        
        _fanParts = parts;

        _blades = blades;
        _body = body;
        _hinge = hinge;

        AttachPartsReferences();

        EnableBladesInteraction();
        EnableBodyInteraction();
        SignHingeStream();
    }

    private void AttachPartsReferences() {
        _blades.Construct(_fanParts.Blades, _fanParts.BladesRollButton);
        _body.Construct(_fanParts.Body, _fanParts.BodyYawButton);
        _hinge.Construct(_fanParts.Hinge, _fanParts.HingePitchButton);
    }

    private void EnableBladesInteraction() {
        _fanParts.BladesRollButton.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!_blades.IsRotating) {
                    _blades.StartRotation();
                } else {
                    _blades.StopRotation();
                }

                _blades.IsRotating = !_blades.IsRotating;
            })
            .AddTo(_disposables);
    }

    private void EnableBodyInteraction() {
        _fanParts.BodyYawButton.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!_body.IsRotating) {
                    _body.StartRotation();
                } else {
                    _body.StopRotation();
                }

                _body.IsRotating = !_body.IsRotating;
            })
            .AddTo(_disposables);
    }

    private void SignHingeStream() {
        _fanParts.HingePitchButton.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!_hinge.IsRotating) {
                    _hinge.StartRotation();
                } else {
                    _hinge.StopRotation();
                }

                _hinge.IsRotating = !_hinge.IsRotating;
            })
            .AddTo(_disposables);
    }
}
