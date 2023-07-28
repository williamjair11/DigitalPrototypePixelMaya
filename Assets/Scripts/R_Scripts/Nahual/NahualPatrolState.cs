using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NahualPatrolState : BaseState
{
    private Nahual _currentContext;

    public NahualPatrolState(StateMachineContext context, NahualStateFactory factory) : base (context, factory)
    {
        _currentContext = (Nahual)context;
    }
    public override void OnStartState()
    {
        Debug.Log("Patrol");
        _currentContext.Animator.SetBool(NahualAnimations.Walk.ToString(), true);
        _currentContext.Agent.isStopped = false;
        _currentContext.Agent.speed = _currentContext.WalkSpeed;
        _currentContext.CurrentDestionation = _currentContext.CurrentWaypoint.position;
    }

    public override void Update()
    {
        if (_currentContext.ObjectType != null)
        {
            if (_currentContext.ObjectType.GetType() == typeof(PlayerController))
            {
                if (_currentContext.Follow && !_currentContext.IsPlayerSafe)
                {
                    SwitchState(_factory.GetState(NahualStates.Follow.ToString()));
                }
            }
            if (_currentContext.ObjectType.GetType() != typeof(TurnOnOffLight))
            {
                if (_currentContext.Follow)
                {
                    SwitchState(_factory.GetState(NahualStates.Follow.ToString()));
                }
            }
        }
        if (Vector3.Distance(_currentContext.transform.position, _currentContext.CurrentWaypoint.position) < _currentContext.DistanceToChangeWaypoint)
        {
            if (!_currentContext.Agent.isStopped)
            {
                _currentContext.ReachWaypoint = true;
                SwitchState(_factory.GetState(NahualStates.Idle.ToString()));
            }
            _currentContext.CurrentWaypoint = _currentContext.Waypoints.GetNextWaypoint(_currentContext.CurrentWaypoint);
            _currentContext.CurrentDestionation = _currentContext.CurrentWaypoint.position;
        } 
    }
    public override void OnExitState()
    {
        _currentContext.Animator.SetBool(NahualAnimations.Walk.ToString(), false);
    }
}
