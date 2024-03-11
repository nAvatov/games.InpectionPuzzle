using UnityEngine;

public interface ITransitionCamera {
    public Camera CameraReference { get; }
    public void MoveToCamera(Transform targetCameraTransform, System.Action OnInspectionStartHandler);
    public void ResetPosition();
}