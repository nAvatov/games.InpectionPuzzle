using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnHoverNotifier : MonoBehaviour {
    [SerializeField] private UI_NotifyPanel _notifyPanel;
    [SerializeField] private string _notificationText;

    private void OnMouseEnter() {
        Debug.Log(_notificationText);
        _notifyPanel.ShowNotify(_notificationText);
    }
    
}
