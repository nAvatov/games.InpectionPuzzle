using System;
using UnityEngine;

[Serializable]
public struct LockedCameraConfiguration {
    public float ZoomInRestriction;
    public float ZoomOutRestriction;
    public float CameraMoveSpeedByButtons;
    public float CameraMoveSpeedByGestures;

    public Vector3 InitialPosition;
    public Vector3 InitialRotation;
    public float RestoreDuration;
}