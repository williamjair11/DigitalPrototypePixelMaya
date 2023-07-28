using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NahualFollowState : BaseState
{
    private Nahual _currentContext;
    public NahualFollowState(StateMachineContext context, StatesFactory factory) : base (context, factory)
    {
        _currentContext = (Nahual)context;
    }
    public override void OnStartState()
    {
        _currentContext.Animator.SetBool(NahualAnimations.Run.ToString(), true);
        Debug.Log("Follow");
        _currentContext.CurrentDestionation = _currentContext.NewDestination;
        _currentContext.Agent.isStopped = false;
        _currentContext.Agent.speed = _currentContext.RunSpeed;
    }

    public override void Update()
    {
        if (_currentContext.ObjectType != null)
        {
            if (_currentContext.ObjectType.GetType() == typeof(PlayerController))
            {
                UpdatePlayerPosition();
                if (Vector3.Distance(_currentContext.transform.position, _currentContext.CurrentDestionation) <= _currentContext.DistanceToAttack && !_currentContext.IsPlayerSafe)
                {
                    SwitchState(_factory.GetState(NahualStates.Attack.ToString()));
                }
                if (_currentContext.IsPlayerSafe)
                {
                    SwitchState(_factory.GetState(NahualStates.Patrol.ToString()));
                }
            }
            if (_currentContext.ObjectType.GetType() == typeof(TurnOnOffLight))
            {
                if (Vector3.Distance(_currentContext.transform.position, _currentContext.CurrentDestionation) == _currentContext.Agent.stoppingDistance)
                {

                }
            }
        }
        else{
            SwitchState(_factory.GetState(NahualStates.Patrol.ToString()));
        }
    }
    public override void OnExitState()
    {
        _currentContext.Animator.SetBool(NahualAnimations.Run.ToString(), false);
    }
    void UpdatePlayerPosition()
    {
        _currentContext.CurrentDestionation = _currentContext.NewDestination;
    }
}
