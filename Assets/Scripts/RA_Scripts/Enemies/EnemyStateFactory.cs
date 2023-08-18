using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateFactory
{

    private BaseState _deafaultStateScript;

    [SerializeField] private List<State> _enemyStates;
    public BaseState getState(string stateId)
    {
        BaseState newState = null;
        foreach(State state in _enemyStates)
        {
            if(state.id == stateId)
            newState = state.stateScript;
        }
        if(newState == null) newState = _deafaultStateScript;
        return newState;
    }

    [Serializable]
    public struct State
    {
        public string id;
        public BaseState stateScript;
    }
}
