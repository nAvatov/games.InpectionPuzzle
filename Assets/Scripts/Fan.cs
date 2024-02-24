using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class Fan : IFanInteractive, ITargetObject {
    private CompositeDisposable _disposables;
    private FanPartsStruct _fanParts;
    public Transform targetTransform => _fanParts.ViewCenter;
    public FanPartsStruct FanParts => _fanParts;

    [Inject]
    public void Construct(CompositeDisposable disposables, FanPartsStruct parts) {
        _disposables = disposables;
        
        _fanParts = parts;

        SignSwitchStream();
    }

    private void SignSwitchStream() {
        _fanParts.SwitchButton.OnMouseDownAsObservable()
            .Subscribe(_ => {
                Debug.Log("Click");
            })
            .AddTo(_disposables);
    }
}
