using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzitzimimeAttackState : BaseState
{
    private Tzitzimime _contextState;
    
    public TzitzimimeAttackState(StateMachineContext currentContext, StatesFactory stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime)currentContext;
    }

    public override void OnStartState()
    {
        _contextState.Agent.isStopped = true;
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Attacking.ToString(),true);
    }

    public override void OnExitState()
    {
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Attacking.ToString(),false);

    }

    public override void Update()
    {
        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Idle)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Idle.ToString()));

        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Walking)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Walking.ToString()));

        if( _contextState.CurrentEnemyState == TzitzimimeStatesId.Following) 
        _contextState.Agent.SetDestination(_contextState.Target.transform.position);

        //if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Attacking)
    }

}
