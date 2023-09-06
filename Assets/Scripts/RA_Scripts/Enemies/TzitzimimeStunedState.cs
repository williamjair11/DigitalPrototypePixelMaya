using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzitzimimeStunedState : BaseState
{
    private Tzitzimime _contextState;
    private float _stunedCounter = 0;
    
    public TzitzimimeStunedState(StateMachineContext currentContext, StatesFactory stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime)currentContext;
    }

    public override void OnStartState()
    {
        _contextState.Agent.isStopped = true;
        _contextState.IsStuned = true;
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Stuned.ToString(), true);
        _contextState.Animator.Play(TzitzimimeAnimationsId.Stuned.ToString());
    }

    public override void OnExitState()
    {
        _contextState.Agent.isStopped = false;
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Stuned.ToString(), false);
        _stunedCounter = 0;
    }

    public override void Update()
    {
        _stunedCounter += Time.deltaTime;
        if(_stunedCounter >= _contextState.StunedTime)
        {
            _contextState.Animator.SetBool(TzitzimimeAnimationsId.Stuned.ToString(), false);
            _contextState.IsStuned = false;
            SwitchState(_factory.GetState(_contextState.CurrentEnemyState.ToString()));
            _stunedCounter = 0;
        }
        
    }
}