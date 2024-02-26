using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_Notificator : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI _notifyTMP;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private float _notificationTimer = 5f;
    private CanvasGroup _cg;

    private void Start() {
        _cg = GetComponent<CanvasGroup>();
    }

    public void ShowNotify(string text) {
        StopAllCoroutines();
        _cg.DOKill();
        
        if (_cg.alpha > 0) {
            HideNotification(() => {
                _notifyTMP.SetText(text);
                StartCoroutine(ShowNotificationTimer());
            });
        } else {
            _notifyTMP.SetText(text);
            StartCoroutine(ShowNotificationTimer());
        }
    }

    private IEnumerator ShowNotificationTimer() {
        _cg.DOFade(1f, _fadeDuration);

        yield return new WaitForSeconds(_notificationTimer);

        // Prevent useless call
        if (_cg.alpha == 1) {
            HideNotification();
        }
    }

    private void HideNotification(System.Action callback = null) {
        _cg.DOFade(0f, _fadeDuration)
            .OnComplete(() => {
                _notifyTMP.SetText("");
                if (callback != null) {
                    callback();
                }
            });
    }

    private void OnDestroy() {
        _cg.DOKill();
    }
}
