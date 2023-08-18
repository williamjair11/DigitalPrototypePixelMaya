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

    public override void OnStartState()
    {
        _contextState.Agent.isStopped = false;
        _contextState.Agent.speed = _contextState.WalkSpeed;
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Walking.ToString(), true);
    }

    public override void OnExitState()
    {
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Walking.ToString(), false);
        
    }

    public override void Update()
    {
        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Walking)
        _contextState.Agent.SetDestination(_contextState.Target.transform.position);

        if(_contextState.IsWalkingToInitialPlace) return;
        
        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Idle)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Idle.ToString()));

        if( _contextState.CurrentEnemyState == TzitzimimeStatesId.Following)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Following.ToString()));

        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Attacking)
        SwitchState(_factory.GetState(TzitzimimeStatesId.Attacking.ToString()));
    }
}
