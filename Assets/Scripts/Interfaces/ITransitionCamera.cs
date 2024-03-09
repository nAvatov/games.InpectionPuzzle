using UnityEngine;

public interface ITransitionCamera {
    public Camera CameraReference { get; }
    public void MoveToCamera(Camera cam, System.Action OnInspectionStartHandler);
    public void ResetPosition();
}