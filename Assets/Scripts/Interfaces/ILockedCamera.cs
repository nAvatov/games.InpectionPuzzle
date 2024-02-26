using UnityEngine;

public interface ILockedCamera {
    public void MoveAroundObject(Vector3 direction);
    public void ChangeDistanceToObject(Vector3 direction);
    public void RotateCameraByGesture(Vector2 gestureStartPos, Vector2 gestureCurrentPos);
    public void RestoreTransform();
}