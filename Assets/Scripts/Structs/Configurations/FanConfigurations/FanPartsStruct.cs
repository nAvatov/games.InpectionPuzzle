using UnityEngine;
using System;

[Serializable]
public struct FanPartsStruct {
    public Transform ViewCenter;
    public PressableButtonStruct BladesRollButton;
    public PressableButtonStruct BodyYawButton;
    public PressableButtonStruct HingePitchButton;
    public Transform Blades;
    public Transform Hinge;
    public Transform Body;
}