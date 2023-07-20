using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzitzimimeWalkState : BaseState
{
    private Tzitzimime _contextState;
    
    public TzitzimimeWalkState(StateMachineContext currentContext, StatesFactory stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime)currentContext;
    }

    public override void Update()
    {
        /*if(!_contextState.Animator.GetBool(TzitzimimeAnimationsId.Walking.ToString()))
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Walking.ToString(), true);*/

        if(_contextState.Looking || _contextState.Greeting)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Idle.ToString()));

        if(_contextState.Walking)
        Debug.Log("InWalkingState");

        if( _contextState.Following)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Following.ToString()));
        if(_contextState.Attacking)
        SwitchState(_factory.GetState(TzitzimimeStatesId.Attacking.ToString()));
    }

    public override void OnExitState()
    {
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Walking.ToString(), false);
        
    }

    public override void OnStartState()
    {
        _contextState.Agent.speed = _contextState.WalkSpeed;
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Walking.ToString(), true);
    }

}
