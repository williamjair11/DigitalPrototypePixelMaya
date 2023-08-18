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

    public override void OnStartState()
    {
        
        _contextState.Agent.isStopped = false;
        _contextState.Agent.speed = _contextState.RunSpeed;
        _contextState.Animator.SetLayerWeight(1, 1);
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Running.ToString(), true);

    }

    public override void OnExitState()
    {
        _contextState.Animator.SetLayerWeight(1, 0);
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Running.ToString(), false);

    }

    public override void Update()
    {
        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Idle)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Idle.ToString()));

        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Walking)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Walking.ToString()));

        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Following) 
        _contextState.Agent.SetDestination(_contextState.Target.transform.position);

        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Attacking)
        SwitchState(_factory.GetState(TzitzimimeStatesId.Attacking.ToString()));
    }
}
