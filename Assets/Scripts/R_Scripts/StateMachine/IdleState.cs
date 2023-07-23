using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BS
{
    private Tzitzimime2 _contextState;
    private float _randomDelay, _targetDistance;
    private float _minDelay = 1, _maxDelay = 7;
    private bool _waitingToChangeIdle = false;
    
    public IdleState(StateMachineCtx currentContext, SF stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime2)currentContext;
    }

    public override void OnStartState()
    {
        //SetRandomIdleAnimation();
    }

    public override void OnExitState()
    {
      //  _contextState.Animator.SetBool(TzitzimimeAnimationsId.Idle.ToString(), false);
    }

    public override void Update()
    {
        if(!_waitingToChangeIdle)
        _contextState.StartCoroutine(IdleRoutine());

        if(_contextState.Looking)
        { 
            _contextState.Animator.SetBool(TzitzimimeAnimations.Greeting.ToString(),false);
        }

        if(_contextState.Greeting)
            _contextState.Animator.SetBool(TzitzimimeAnimations.Greeting.ToString(), true);

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

    public IEnumerator IdleRoutine()
    {
        _waitingToChangeIdle = true;
        _randomDelay = Random.Range(_minDelay, _maxDelay);
        yield return new WaitForSeconds(_randomDelay);
        _waitingToChangeIdle = false;
    }
}
