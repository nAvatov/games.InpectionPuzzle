using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[Serializable]
public struct CommonInspectionUiStruct {
    public CanvasGroup _mainCanvasGroup;
    public float _fadeDuration;
    public Button _stopInspectionButton;

    public void FadeInInspectionUI() {
        _mainCanvasGroup.DOKill();
        CanvasGroup cg = _mainCanvasGroup;

        cg.DOFade(1f, _fadeDuration)
            .OnComplete(() => {
                cg.interactable = true;
                cg.blocksRaycasts = true;
                cg.DOKill();
            });
    }

    public void FadeOutInspectionUI() {
        _mainCanvasGroup.DOKill();
        CanvasGroup cg = _mainCanvasGroup;

        _mainCanvasGroup.interactable = false;
        _mainCanvasGroup.blocksRaycasts = false;

        _mainCanvasGroup.DOFade(0f, _fadeDuration)
            .OnComplete(() => cg.DOKill());
    }
}