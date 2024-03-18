using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateFans : CommonStateFSM {
    public GlobalStateFans(CommonFSM fsm) : base(fsm) {}

    public override void Enter() {
        Debug.Log("State fans!");
    }

    public override void Exit() {
        Debug.Log("State fans is complete");
    }

    public override void Update() {
        
    }
}
