using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateBeginning : CommonStateFSM {
    public GlobalStateBeginning(CommonFSM fsm) : base(fsm) {}

    public override void Enter() {
        Debug.Log("State beggining!");
    }

    public override void Exit() {
        Debug.Log("State beggining is complete");
    }

    public override void Update() {
        
    }
}
