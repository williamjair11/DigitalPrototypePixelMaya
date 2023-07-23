using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BS : MonoBehaviour
{
    private StateMachineCtx _context;
    protected SF _factory;

    public BS(StateMachineCtx currentContext, SF stateFactory)
    {
        _context = currentContext;
        _factory = stateFactory;
    }
    
    public abstract void OnStartState();
    public abstract void OnExitState();

    public abstract void Update();

    protected void SwitchState(BS newState){
        _context.CurrentState.OnExitState();
        _context.CurrentState = newState;
        _context.CurrentState.OnStartState();
    }
}
