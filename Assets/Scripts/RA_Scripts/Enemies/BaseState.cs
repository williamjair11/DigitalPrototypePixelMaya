using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    private StateMachineContext _context;
    protected StatesFactory _factory;

    public BaseState(StateMachineContext currentContext, StatesFactory stateFactory)
    {
        _context = currentContext;
        _factory = stateFactory;
    }
    
    public abstract void OnStartState();
    public abstract void OnExitState();

    public abstract void Update();

    protected void SwitchState(BaseState newState){
        _context.CurrentState.OnExitState();
        _context.CurrentState = newState;
        _context.CurrentState.OnStartState();
    }
}
