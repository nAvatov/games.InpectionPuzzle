using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class FanController : IFanInteractive, ITargetObject {
    private CompositeDisposable _disposables;
    private FanPartsStruct _fanParts;
    private Blades _blades;
    private Body _body;
    private Hinge _hinge;
    [Inject] private FanAudioSourcesStruct _fanAudioSources;
    [Inject] private FanSoundsConfiguration _soundConfiguration;

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

        EnableFanInteraction(_fanParts.BladesRollButton.ButtonCollider, _blades);
        EnableFanInteraction(_fanParts.BodyYawButton.ButtonCollider, _body);
        EnableFanInteraction(_fanParts.HingePitchButton.ButtonCollider, _hinge);
    }

    private void AttachPartsReferences() {
        _blades.Construct(_fanParts.Blades, _fanParts.BladesRollButton);
        _body.Construct(_fanParts.Body, _fanParts.BodyYawButton);
        _hinge.Construct(_fanParts.Hinge, _fanParts.HingePitchButton);
    }

    private void EnableFanInteraction(Collider interactionObject, IRotatableFanPart part) {
        interactionObject.OnMouseDownAsObservable()
            .Subscribe(_ => {
                if (!part.IsRotating) {
                    part.StartRotation();
                } else {
                    part.StopRotation();
                }

                _fanAudioSources.ButtonsAudioSource.PlayOneShot(_soundConfiguration.ButtonClick);
                part.IsRotating = !part.IsRotating;
            })
            .AddTo(_disposables);
    }
}
