using UnityEngine;

public interface IRotatableFanPart {
    public bool IsRotating { get; set; }
    public Transform Part { get; }
    public Collider Button { get; }

    public void Construct(Transform part, Collider button);

    public void StartRotation();
    public void StopRotation();
}