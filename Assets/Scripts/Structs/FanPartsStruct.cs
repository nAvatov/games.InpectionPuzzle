using UnityEngine;
using System;

[Serializable]
public struct FanPartsStruct {
    public Transform ViewCenter;
    public Collider SwitchButton;
    public Collider BodyRotationButton;
    public Transform FanBlades;
    public Transform Hinge;
    public Transform Body;
}