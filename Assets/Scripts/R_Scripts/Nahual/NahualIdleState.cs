using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NahualIdleState : BaseState
{
    private Nahual _currentContext;
    public NahualIdleState(StateMachineContext context, StatesFactory factory) : base (context, factory)
    {
        _currentContext = (Nahual)context;
    }
    public override void OnStartState()
    {
        Debug.Log("Idle");
        _currentContext.Idle = true;
        _currentContext.Agent.speed = _currentContext.IdleSpeed;
        _currentContext.Agent.isStopped = true;
        _currentContext.Agent.SetDestination(_currentContext.transform.position);
        if (_currentContext.ReachWaypoint)
        {
            _currentContext.StartCoroutine(RandomIdleTime());
        }
    }

    public override void Update()
    {
        if (!_currentContext.ReachWaypoint || !_currentContext.Idle)
        {
            SwitchState(_factory.GetState(NahualStates.Patrol.ToString()));
        }
        if (_currentContext.Follow)
        {
            SwitchState(_factory.GetState(NahualStates.Follow.ToString()));
        }
        if (_currentContext.Atack)
        {
            SwitchState(_factory.GetState(NahualStates.Attack.ToString()));
        }  
    }
    public override void OnExitState()
    {
        _currentContext.Agent.isStopped = false;
    }

    IEnumerator RandomIdleTime()
    {
        float randomtime = Random.Range(2, 5);
        yield return new WaitForSeconds(randomtime);
        _currentContext.ReachWaypoint = false;
    }

}
