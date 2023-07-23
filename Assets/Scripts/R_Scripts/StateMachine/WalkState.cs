using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BS
{
    private Tzitzimime2 _contextState;
    
    public WalkState(StateMachineCtx currentContext, SF stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime2)currentContext;
    }

    public override void Update()
    {
        /*if(!_contextState.Animator.GetBool(TzitzimimeAnimationsId.Walking.ToString()))
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Walking.ToString(), true);*/

        if(_contextState.Looking || _contextState.Greeting)
            SwitchState(_factory.GetState(TzitzimimeAnimations.Idle.ToString()));

        if(_contextState.Walking)
        Debug.Log("InWalkingState");

        if( _contextState.Following)
            SwitchState(_factory.GetState(TzitzimimeAnimations.Following.ToString()));
        if(_contextState.Attacking)
        SwitchState(_factory.GetState(TzitzimimeAnimations.Attacking.ToString()));
    }

    public override void OnExitState()
    {
        _contextState.Animator.SetBool(TzitzimimeAnimations.Walking.ToString(), false);
        
    }

    public override void OnStartState()
    {
        _contextState.Agent.speed = _contextState.WalkSpeed;
        _contextState.Animator.SetBool(TzitzimimeAnimations.Walking.ToString(), true);
    }

}
