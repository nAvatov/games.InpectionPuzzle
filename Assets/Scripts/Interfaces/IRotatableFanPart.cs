public interface IRotatableFanPart {
    public bool IsRotating { get; }

    public void StartRotation();
    public void StopRotation();
}