using UnityEngine;
using System;

[Serializable]
public struct FanPartsStruct {
    public Transform ViewCenter;
    public Collider SwitchButton;
    public Collider VerticalTiltButton;
    public Transform FanBlades;
    public Transform Hinge;
}