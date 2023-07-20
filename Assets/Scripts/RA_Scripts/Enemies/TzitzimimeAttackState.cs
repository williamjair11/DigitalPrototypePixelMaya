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
        Debug.Log("AtackingShouldBeTrueStart");
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Attacking.ToString(),true);
    }

    public override void OnExitState()
    {
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Attacking.ToString(),false);

    }

    public override void Update()
    {
        Debug.Log("AtackingShouldBeTrue");
        if(_contextState.Looking || _contextState.Greeting)
             SwitchState(_factory.GetState(TzitzimimeStatesId.Idle.ToString()));

        if(_contextState.Walking)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Walking.ToString()));
        

        if( _contextState.Following)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Following.ToString()));
        
    }

    public void SetRandomIdleAnimation()
    {
        int randomId = Random.Range(0, _contextState.IdleAnimationsCount);
        _contextState.Animator.SetInteger(
        TzitzimimeAnimationsId.Idle.ToString(), randomId
        );
    }
}
