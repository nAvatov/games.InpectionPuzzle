using System;
using System.Collections.Generic;

public abstract class CommonFSM {
    private protected CommonStateFSM _currentState;
    private Dictionary<Type, CommonStateFSM> _states = new Dictionary<Type, CommonStateFSM>();

    public void AddNewState(CommonStateFSM state) {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T: CommonStateFSM {
        if (_currentState != null && _currentState.GetType() == typeof(T)) {
            return;
        }

        if (_states.TryGetValue(typeof(T), out var cachedState)) {
            _currentState?.Exit();
            _currentState = cachedState;
            _currentState.Enter();
        }
    }
}