using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzitzimimeFollowState : BaseState
{
    private Tzitzimime _contextState;
    
    public TzitzimimeFollowState(StateMachineContext currentContext, StatesFactory stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime)currentContext;
    }

    public override void Update()
    {
        if(_contextState.Looking || _contextState.Greeting)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Idle.ToString()));

        if(_contextState.Walking)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Walking.ToString()));

        if( _contextState.Following) Debug.Log("InFollowingState");

        if(_contextState.Attacking)
        SwitchState(_factory.GetState(TzitzimimeStatesId.Attacking.ToString()));
    }

    public override void OnExitState()
    {
        _contextState.Animator.SetLayerWeight(1, 0);
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Running.ToString(), false);

    }

    public override void OnStartState()
    {
        _contextState.Agent.speed = _contextState.RunSpeed;
        _contextState.Animator.SetLayerWeight(1, 1);
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Running.ToString(), true);

    }

}
