using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonStateFSM {
    protected readonly CommonFSM _fsmInstance;

    public CommonStateFSM(CommonFSM fsm) {
        _fsmInstance = fsm;
    }

    public virtual void Enter() {}
    public virtual void Update() {}
    public virtual void Exit() {}
}
