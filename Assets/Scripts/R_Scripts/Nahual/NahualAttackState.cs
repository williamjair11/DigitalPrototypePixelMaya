using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NahualAttackState : BaseState
{
    private Nahual _currentContext;
    public NahualAttackState(StateMachineContext context, StatesFactory factory) : base (context, factory)
    {
        _currentContext = (Nahual)context;
    }
    public override void OnStartState()
    {
        _currentContext.Animator.SetBool(NahualAnimations.Attack.ToString(), true);
        Debug.Log("Attack");
        _currentContext.Agent.speed = _currentContext.IdleSpeed;
        _currentContext.Agent.isStopped = true;
        _currentContext.CurrentDestionation = _currentContext.transform.position;
        _currentContext.Agent.stoppingDistance = 3f;
    }

    public override void Update()
    {
        if (_currentContext.PlayerController != null)
        {
            UpdatePlayerPosition();
        }
        if (Vector3.Distance(_currentContext.transform.position, _currentContext.NewDestination) >= _currentContext.DistanceToAttack && Vector3.Distance(_currentContext.transform.position, _currentContext.NewDestination) <= _currentContext.DistanceToFollow)
        {
            SwitchState(_factory.GetState(NahualStates.Follow.ToString()));
        }
        if (!_currentContext.Follow && Vector3.Distance(_currentContext.transform.position, _currentContext.NewDestination) > _currentContext.DistanceToFollow)
        {
            SwitchState(_factory.GetState(NahualStates.Patrol.ToString()));
        }
    }
    public override void OnExitState()
    {
        _currentContext.Animator.SetBool(NahualAnimations.Attack.ToString(), false);
    }
    void UpdatePlayerPosition()
    {
        _currentContext.CurrentDestionation = _currentContext.NewDestination;
    }
}
