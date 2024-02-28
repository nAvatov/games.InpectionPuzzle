using UnityEngine;

public interface ITransitionCamera {
    public Camera CameraReference { get; }
    public void MoveToCamera(Camera cam);
    public void ResetPosition();
}