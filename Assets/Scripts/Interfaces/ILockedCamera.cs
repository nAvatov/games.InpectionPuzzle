using UnityEngine;

public interface ILockedCamera {
    public void MoveAroundObject(Vector3 direction);
    public void ChangeDistanceToObject(Vector3 direction);
}