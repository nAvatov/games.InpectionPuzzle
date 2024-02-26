using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class UI_OnHoverNotifier : MonoBehaviour {
    [Inject] private HoverConfigurationStruct _hoverConfiguration;
    [Inject] private UI_Notificator _notificator;
    [SerializeField] private string _notificationText;
    private MeshRenderer _meshRend;
    private Color _defaulColor;

    private void Start() {
        _meshRend = GetComponent<MeshRenderer>();
        _defaulColor = _meshRend.material.color;
    }

    private void OnMouseEnter() {
        _meshRend.material.DOColor(_hoverConfiguration.HoverEmissionColor, _hoverConfiguration.HoverEffectDuration);
        _notificator.ShowNotify(_notificationText);
    }

    private void OnMouseExit() {
        _meshRend.material.DOColor(_defaulColor, _hoverConfiguration.HoverEffectDuration);
    }
}
