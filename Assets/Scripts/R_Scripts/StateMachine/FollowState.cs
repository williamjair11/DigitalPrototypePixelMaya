using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : BS
{
    private Tzitzimime2 _contextState;
    
    public FollowState(StateMachineCtx currentContext, SF stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime2)currentContext;
    }

    public override void Update()
    {
        if(_contextState.Looking || _contextState.Greeting)
            SwitchState(_factory.GetState(TzitzimimeAnimations.Idle.ToString()));

        if(_contextState.Walking)
            SwitchState(_factory.GetState(TzitzimimeAnimations.Walking.ToString()));

        if( _contextState.Following) Debug.Log("InFollowingState");

        if(_contextState.Attacking)
        SwitchState(_factory.GetState(TzitzimimeAnimations.Attacking.ToString()));
    }

    public override void OnExitState()
    {
        _contextState.Animator.SetLayerWeight(1, 0);
        _contextState.Animator.SetBool(TzitzimimeAnimations.Running.ToString(), false);

    }

    public override void OnStartState()
    {
        _contextState.Agent.speed = _contextState.RunSpeed;
        _contextState.Animator.SetLayerWeight(1, 1);
        _contextState.Animator.SetBool(TzitzimimeAnimations.Running.ToString(), true);

    }

}
