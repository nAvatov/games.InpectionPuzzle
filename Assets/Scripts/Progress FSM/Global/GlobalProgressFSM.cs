using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalProgressFSM : CommonFSM, IInitializable, ITickable {
    public void Initialize() {
        AddNewState(new GlobalStateBeginning(this));
        AddNewState(new GlobalStateFans(this));

        SetState<GlobalStateBeginning>();
    }

    public void Tick() {
        _currentState.Update();
    }
}
