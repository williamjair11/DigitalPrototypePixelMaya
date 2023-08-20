using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzitzimimeIdleState : BaseState
{
    private Tzitzimime _contextState;
    private float _randomDelay, _targetDistance;
    private float _minDelay = 1, _maxDelay = 7;
    private bool _waitingToChangeIdle = false;
    
    public TzitzimimeIdleState(StateMachineContext currentContext, StatesFactory stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime)currentContext;
    }

    public override void OnStartState()
    {
       _contextState.Agent.isStopped = true;
    }

    public override void OnExitState()
    {
      //  _contextState.Animator.SetBool(TzitzimimeAnimationsId.Idle.ToString(), false);
    }

    public override void Update()
    {
        if(!_waitingToChangeIdle)
        _contextState.StartCoroutine(IdleRoutine());

        if(_contextState.TargetIsInGreetingRange)
        {
            _contextState.transform.LookAt(GameManager.Instance.playerController.transform);
            _contextState.Animator.SetBool(TzitzimimeAnimationsId.Greeting.ToString(), true);
        }
        else
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Greeting.ToString(),false);
        
        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Walking)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Walking.ToString()));
        
        if( _contextState.CurrentEnemyState == TzitzimimeStatesId.Following)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Following.ToString()));
        
    }

    public void SetRandomIdleAnimation()
    {
        int randomId = Random.Range(0, _contextState.IdleAnimationsCount);
        _contextState.Animator.SetInteger(
        TzitzimimeAnimationsId.Idle.ToString(), randomId
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
