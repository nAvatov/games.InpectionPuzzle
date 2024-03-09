using UnityEngine;
using System;

[Serializable]
public struct FanPartsStruct {
    public Transform ViewCenter;
    public Collider BladesRollButton;
    public Collider BodyYawButton;
    public Collider HingePitchButton;
    public Transform Blades;
    public Transform Hinge;
    public Transform Body;
}