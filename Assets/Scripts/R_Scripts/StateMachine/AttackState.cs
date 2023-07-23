using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BS
{
    private Tzitzimime2 _contextState;
    
    public AttackState(StateMachineCtx currentContext, SF stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime2)currentContext;
    }

    public override void OnStartState()
    {
        Debug.Log("AtackingShouldBeTrueStart");
        _contextState.Animator.SetBool(TzitzimimeAnimations.Attacking.ToString(),true);
    }

    public override void OnExitState()
    {
        _contextState.Animator.SetBool(TzitzimimeAnimations.Attacking.ToString(),false);

    }

    public override void Update()
    {
        Debug.Log("AtackingShouldBeTrue");
        if(_contextState.Looking || _contextState.Greeting)
             SwitchState(_factory.GetState(TzitzimimeAnimations.Idle.ToString()));

        if(_contextState.Walking)
            SwitchState(_factory.GetState(TzitzimimeAnimations.Walking.ToString()));
        

        if( _contextState.Following)
            SwitchState(_factory.GetState(TzitzimimeAnimations.Following.ToString()));
        
    }

    public void SetRandomIdleAnimation()
    {
        int randomId = Random.Range(0, _contextState.IdleAnimationsCount);
        _contextState.Animator.SetInteger(
        TzitzimimeAnimations.Idle.ToString(), randomId
        );
    }
}

